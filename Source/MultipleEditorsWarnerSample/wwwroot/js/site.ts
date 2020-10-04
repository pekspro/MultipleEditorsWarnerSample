window.addEventListener("load", function () {

    const noScrollObjects = new Array<HTMLElement>(0);
    
    const callback = function (e: Event) {

        const target = e.target as HTMLElement;

        if (target.scrollHeight - target.scrollTop - target.clientHeight < 1) {

            // Scrolled to bottom
            const index = noScrollObjects.indexOf(target);
            if (index > -1) {
                noScrollObjects.splice(index);
            }

        } else {

            // Scroll somewhere else. Prevent scrolling.
            noScrollObjects.push(target);

        }
    }
        
    // Create an observer instance linked to the callback function
    // Read more: https://developer.mozilla.org/en-US/docs/Web/API/MutationObserver
    const observer = new MutationObserver(
        function (mutations: MutationRecord[]) {
            const objectsToScroll = new Array<HTMLElement>(0);

            for (const mutation of mutations) {
                if (mutation.type === 'childList' && mutation.addedNodes.length > 0) {
                    const target = mutation.target as HTMLElement;

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
            for (const objectToScroll of objectsToScroll) {
                objectToScroll.scrollTop = objectToScroll.scrollHeight;
            }
        }
    );

    // Only observe changes in nodes in the whole tree, but do not observe attributes.
    const observerConfig = { subtree: true, childList: true, attributes: false };

    // Start observing the target node for configured mutations
    observer.observe(document, observerConfig);
});
