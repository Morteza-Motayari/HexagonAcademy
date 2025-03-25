$("#reload-captcha").click(function () {
    resetCaptchaImage();
});

function resetCaptchaImage() {
    d = new Date();
    $("#img-captcha").attr("src", "/get-captcha-image?" + d.getTime());
}