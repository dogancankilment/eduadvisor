function InfoBox(opt_opts) {
    opt_opts = opt_opts || {};
    this.imgPath = 'Content/img/infoBox/';
    google.maps.OverlayView.apply(this, arguments);
    // Standard options (in common with google.maps.InfoWindow):
    this.content_ = opt_opts.content || "";
    this.maxWidth_ = opt_opts.maxWidth || 0;
    this.pixelOffset_ = opt_opts.pixelOffset || new google.maps.Size(0, 0);
    this.position_ = opt_opts.position || new google.maps.LatLng(0, 0);
    this.zIndex_ = opt_opts.zIndex || null;

    // Additional options (unique to InfoBox):
    this.boxStyle_ = opt_opts || {};
    this.infoBoxClearance_ = new google.maps.Size(1, 1);
    this.isHidden_ = opt_opts.isHidden || false;
    this.pane_ = "overlayMouseTarget";
    this.enableEventPropagation_ = opt_opts.enableEventPropagation || false;
    this.div_ = null;
    this.closeListener_ = null;
    this.eventListener1_ = null;
    this.eventListener2_ = null;
    this.eventListener3_ = null;
    this.contextListener_ = null;
    this.fixedWidthSet_ = null;
}

/* InfoBox extends OverlayView in the Google Maps API v3. */
InfoBox.prototype = new google.maps.OverlayView();

// Creates the DIV representing the InfoBox. @private
InfoBox.prototype.createInfoBoxDiv_ = function () {
    var bw, me = this;

    // This handler prevents an event in the InfoBox from being passed on to the map.
    var cancelHandler = function (e) { e.cancelBubble = true; if (e.stopPropagation) e.stopPropagation(); };
    // This handler ignores the current event in the InfoBox and conditionally prevents the event from being passed on to the map. It is used for the contextmenu event.
    var ignoreHandler = function (e) { e.returnValue = false; if (e.preventDefault) e.preventDefault(); if (!me.enableEventPropagation_) { e.cancelBubble = true; if (e.stopPropagation) e.stopPropagation(); } };

    if (!this.div_) {    // first time create
        this.div_ = document.createElement("div");
        this.div_.className = 'infowindow';
        this.setBoxStyle_();

        // Apply required styles:
        if (this.zIndex_ !== null) this.div_.style.zIndex = this.zIndex_;

        this.div_.contentDiv = document.createElement('div');
        this.div_.contentDiv.className = 'infowindow-wrapper';
        this.div_.contentDiv.innerHTML = this.content_;
        this.div_.innerHTML = '<img src="' + this.imgPath + 'close.png" align="right" class="infowindow-close">';
        this.div_.appendChild(this.div_.contentDiv);

        // Add the InfoBox DIV to the DOM
        this.getPanes()[this.pane_].appendChild(this.div_);
        this.addClickHandler_();
        if (this.div_.style.width) this.fixedWidthSet_ = true;
        else {
            if (this.maxWidth_ !== 0 && this.div_.offsetWidth > this.maxWidth_) {
                this.div_.style.width = this.maxWidth_;
                this.fixedWidthSet_ = true;
            }
            else { // The following code is needed to overcome problems with MSIE
                bw = this.getBoxWidths_();
                this.div_.style.width = (this.div_.offsetWidth - bw.left - bw.right) + "px";
                this.fixedWidthSet_ = false;
            }
        }
        if (!this.enableEventPropagation_) {
            this.eventListener1_ = google.maps.event.addDomListener(this.div_.contentDiv, "mousedown", cancelHandler);
            this.eventListener2_ = google.maps.event.addDomListener(this.div_, "click", function (e) {
                e.cancelBubble = true; if (e.stopPropagation) e.stopPropagation();
                if (GoogleMap && GoogleMap.closeEditors) GoogleMap.closeEditors(true);
            });
            //this.eventListener3_ = google.maps.event.addDomListener(this.div_, "dblclick", cancelHandler);
            try { this.eventListener3_ = google.maps.event.addDomListener(this.div_, "dblclick", facilityEditor); } catch (e) { }
        }
        this.contextListener_ = google.maps.event.addDomListener(this.div_, "contextmenu", ignoreHandler);

        var contentWidth = parseInt(this.div_.style.width.slice(0, -2)), contentHeight = this.div_.offsetHeight;
        this.wrapperParts = { //create an object to reference each image
            tl: { l: -26, t: -26, w: 26, h: 26 },
            t: { l: 0, t: -26, w: contentWidth, h: 26 },
            tr: { l: contentWidth, t: -26, w: 26, h: 26 },
            l: { l: -26, t: 0, w: 26, h: contentHeight },
            r: { l: contentWidth, t: 0, w: 26, h: contentHeight },
            bl: { l: -26, t: contentHeight, w: 26, h: 26 },
            b: { l: 0, t: contentHeight, w: contentWidth, h: 26 },
            br: { l: contentWidth, t: contentHeight, w: 26, h: 26 },
            p: { l: contentWidth - 170, t: contentHeight + 18, w: 92, h: 77 }
        }
        for (i in this.wrapperParts) { //create the image DOM objects
            var img = document.createElement('img');
            img.src = this.imgPath + i + '.png'; //load the image from your local image directory based on the property name of the wrapperParts object
            img.style.position = 'absolute'; //set the appropriate positioning attributes
            img.style.top = this.wrapperParts[i].t + 'px';
            img.style.left = this.wrapperParts[i].l + 'px';
            img.style.width = this.wrapperParts[i].w + 'px';
            img.style.height = this.wrapperParts[i].h + 'px';
            this.div_.appendChild(img);
            this.wrapperParts[i].img = img;
        }
        google.maps.event.trigger(this, "domready");
    }

    else {
        var contentWidth = parseInt(this.div_.style.width.slice(0, -2)), contentHeight = this.div_.offsetHeight, twp = this.wrapperParts;
        twp.t.img.style.width = contentWidth + 'px';
        twp.tr.img.style.left = contentWidth + 'px';
        twp.l.img.style.height = contentHeight + 'px';
        twp.r.img.style.left = contentWidth + 'px';
        twp.r.img.style.height = contentHeight + 'px';
        twp.bl.img.style.top = contentHeight + 'px';
        twp.b.img.style.top = contentHeight + 'px';
        twp.b.img.style.width = contentWidth + 'px';
        twp.br.img.style.left = contentWidth + 'px';
        twp.br.img.style.top = contentHeight + 'px';
        twp.p.img.style.left = (contentWidth - 170) + 'px';
        twp.p.img.style.top = (contentHeight + 18) + 'px';
    }
};

InfoBox.prototype.addClickHandler_ = function () {
    this.closeListener_ = google.maps.event.addDomListener(this.div_.firstChild, 'click', this.getCloseClickHandler_());
    try { this.eventListener3_ = google.maps.event.addDomListener(this.div_, "dblclick", facilityEditor); } catch (e) { }
};
InfoBox.prototype.getCloseClickHandler_ = function () { var me = this; return function () { me.close(); google.maps.event.trigger(me, "closeclick"); }; };

//Pans the map so that the InfoBox appears entirely within the map's visible area. @private
InfoBox.prototype.panBox_ = function (disablePan) {
    if (!disablePan) {
        var map = this.getMap();
        var bounds = map.getBounds();
        // The degrees per pixel
        var mapDiv = map.getDiv();
        var mapWidth = mapDiv.offsetWidth;
        var mapHeight = mapDiv.offsetHeight;
        var boundsSpan = bounds.toSpan();
        var longSpan = boundsSpan.lng();
        var latSpan = boundsSpan.lat();
        var degPixelX = longSpan / mapWidth;
        var degPixelY = latSpan / mapHeight;

        // The bounds of the map
        var mapWestLng = bounds.getSouthWest().lng();
        var mapEastLng = bounds.getNorthEast().lng();
        var mapNorthLat = bounds.getNorthEast().lat();
        var mapSouthLat = bounds.getSouthWest().lat();

        // The bounds of the box
        var position = this.position_;
        var iwOffsetX = this.pixelOffset_.width;
        var iwOffsetY = this.pixelOffset_.height;
        var padX = this.infoBoxClearance_.width;
        var padY = this.infoBoxClearance_.height;

        var iwWestLng = position.lng() + (iwOffsetX - padX - this.div_.contentDiv.offsetWidth / 2 - 450) * degPixelX; // 450 - move right - from under the sidebar
        var iwEastLng = position.lng() + (iwOffsetX + padX + 220) * degPixelX;
        var iwNorthLat = position.lat() - (iwOffsetY - padY - this.div_.contentDiv.offsetHeight - 180) * degPixelY; // 180 - move down - from under the top search bar
        var iwSouthLat = position.lat() - (iwOffsetY + padY + 20) * degPixelY;

        // Calculate center shift
        var shiftLng = (iwWestLng < mapWestLng ? mapWestLng - iwWestLng : 0) + (iwEastLng > mapEastLng ? mapEastLng - iwEastLng : 0);
        var shiftLat = (iwNorthLat > mapNorthLat ? mapNorthLat - iwNorthLat : 0) + (iwSouthLat < mapSouthLat ? mapSouthLat - iwSouthLat : 0);
        if (!(shiftLat === 0 && shiftLng === 0)) {
            // Move the map to the new shifted center.
            var c = map.getCenter();
            map.setCenter(new google.maps.LatLng(c.lat() - shiftLat, c.lng() - shiftLng));
        }
    }
};

// Sets the style of the InfoBox. @private
InfoBox.prototype.setBoxStyle_ = function () {
    var i;
    var boxStyle = this.boxStyle_;
    for (i in boxStyle) if (boxStyle.hasOwnProperty(i)) this.div_.style[i] = boxStyle[i];
    // Fix up opacity style for benefit of MSIE:
    if (typeof this.div_.style.opacity !== "undefined") this.div_.style.filter = "alpha(opacity=" + (this.div_.style.opacity * 100) + ")";
};

// Get the widths of the borders of the InfoBox. @private; @return {Object} widths object (top, bottom left, right)
InfoBox.prototype.getBoxWidths_ = function () {
    var computedStyle;
    var bw = { top: 0, bottom: 0, left: 0, right: 0 };
    var box = this.div_;
    if (document.defaultView && document.defaultView.getComputedStyle) {
        computedStyle = box.ownerDocument.defaultView.getComputedStyle(box, "");
        if (computedStyle) {
            // The computed styles are always in pixel units (good!)
            bw.top = parseInt(computedStyle.borderTopWidth, 10) || 0;
            bw.bottom = parseInt(computedStyle.borderBottomWidth, 10) || 0;
            bw.left = parseInt(computedStyle.borderLeftWidth, 10) || 0;
            bw.right = parseInt(computedStyle.borderRightWidth, 10) || 0;
        }
    }
    else if (document.documentElement.currentStyle) { // MSIE
        if (box.currentStyle) {
            // The current styles may not be in pixel units, but assume they are (bad!)
            bw.top = parseInt(box.currentStyle.borderTopWidth, 10) || 0;
            bw.bottom = parseInt(box.currentStyle.borderBottomWidth, 10) || 0;
            bw.left = parseInt(box.currentStyle.borderLeftWidth, 10) || 0;
            bw.right = parseInt(box.currentStyle.borderRightWidth, 10) || 0;
        }
    }
    return bw;
};

// Invoked when <tt>close</tt> is called. Do not call it directly.
InfoBox.prototype.onRemove = function () {
    if (this.div_) {
        this.shadowContainer_.parentNode.removeChild(this.shadowContainer_);
        this.div_.parentNode.removeChild(this.div_);
        this.div_ = null;
    }
};

//Draws the InfoBox based on the current map projection and zoom level.
InfoBox.prototype.draw = function () {
    this.createInfoBoxDiv_();
    var pixPosition = this.getProjection().fromLatLngToDivPixel(this.position_);

    this.div_.style.left = (pixPosition.x - this.div_.offsetWidth + 180) + "px";
    this.div_.style.top = (pixPosition.y - this.div_.offsetHeight - 125) + "px";

    if (this.isHidden_) this.div_.style.visibility = 'hidden';
    else this.div_.style.visibility = "visible";

    this.panBox_();
};


InfoBox.prototype.setContent = function (content) {
    this.content_ = content;
    if (this.div_) {// Odd code required to make things work with MSIE.
        this.div_.style.visibility = 'hidden'
        if (!this.fixedWidthSet_) this.div_.style.width = "";
        this.div_.contentDiv.innerHTML = content;
        // Perverse code required to make things work with MSIE. (Ensures the close box does, in fact, float to the right.)
        if (!this.fixedWidthSet_) { this.div_.style.width = this.div_.offsetWidth + "px"; this.div_.contentDiv.innerHTML = content; }
        this.addClickHandler_();
    }
    // This event is fired when the content of the InfoBox changes. @name InfoBox#content_changed; @event
    google.maps.event.trigger(this, "content_changed");
};

//Sets the geographic location of the InfoBox. @param {LatLng} latlng
InfoBox.prototype.setPosition = function (latlng) {
    this.position_ = latlng;
    if (this.div_) this.draw();
    //This event is fired when the position of the InfoBox changes. @name InfoBox#position_changed;  @event
    google.maps.event.trigger(this, "position_changed");
};


InfoBox.prototype.getContent = function () { return this.content_; }; //Returns the content of the InfoBox. @returns {string}
InfoBox.prototype.show = function () { this.isHidden_ = false; this.div_.style.visibility = "visible"; }; //Shows the InfoBox.
InfoBox.prototype.hide = function () { this.isHidden_ = true; this.div_.style.visibility = "hidden"; }; //Hides the InfoBox.

InfoBox.prototype.open = function (map, anchor) {
    if (anchor) this.position_ = anchor.getPosition();
    this.setMap(map);
};

//Removes the InfoBox from the map.
InfoBox.prototype.close = function () {
    if (this.closeListener_) {
        google.maps.event.removeListener(this.closeListener_);
        this.closeListener_ = null;
    }
    if (this.contextListener_) {
        google.maps.event.removeListener(this.contextListener_);
        this.contextListener_ = null;
    }

    this.setMap(null);
};