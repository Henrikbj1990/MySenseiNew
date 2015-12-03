function init() {
    window.addEventListener('scroll', function (e) {
        var distanceY = window.pageYOffset || document.documentElement.scrollTop,
            shrinkOn = 70,
            header = document.querySelector("header");
        if (distanceY > shrinkOn) {
            classie.add(header, "small");
        } else {
            if (classie.has(header, "small")) {
                classie.remove(header, "small");
            }
        }
    });
}
window.onload = init();