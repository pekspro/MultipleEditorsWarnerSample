window.addEventListener("load", function () {
    var noScrollObjects = new Array(0);
    var callback = function (e) {
        var target = e.target;
        if (target.scrollHeight - target.scrollTop - target.clientHeight < 1) {
            // Scrolled to bottom
            var index = noScrollObjects.indexOf(target);
            if (index > -1) {
                noScrollObjects.splice(index);
            }
        }
        else {
            // Scroll somewhere else. Prevent scrolling.
            noScrollObjects.push(target);
        }
    };
    // Create an observer instance linked to the callback function
    // Read more: https://developer.mozilla.org/en-US/docs/Web/API/MutationObserver
    var observer = new MutationObserver(function (mutations) {
        var objectsToScroll = new Array(0);
        for (var _i = 0, mutations_1 = mutations; _i < mutations_1.length; _i++) {
            var mutation = mutations_1[_i];
            if (mutation.type === 'childList' && mutation.addedNodes.length > 0) {
                var target = mutation.target;
                if (noScrollObjects.indexOf(target) >= 0) {
                    // Scrolling prevented
                    continue;
                }
                if (target && target.classList.contains("auto-scroll-to-bottom")) {
                    // Also add event listener
                    target.onscroll = callback;
                    if (objectsToScroll.indexOf(target) < 0) {
                        objectsToScroll.push(target);
                    }
                }
            }
        }
        // Scroll all potential objects.
        for (var _a = 0, objectsToScroll_1 = objectsToScroll; _a < objectsToScroll_1.length; _a++) {
            var objectToScroll = objectsToScroll_1[_a];
            objectToScroll.scrollTop = objectToScroll.scrollHeight;
        }
    });
    // Only observe changes in nodes in the whole tree, but do not observe attributes.
    var observerConfig = { subtree: true, childList: true, attributes: false };
    // Start observing the target node for configured mutations
    observer.observe(document, observerConfig);
});
//# sourceMappingURL=site.js.map