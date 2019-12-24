var productImageID = [56, 57, 58, 59, 60, 66, 67, 68, 69, 70]
var imagePath = "/images/img-p-"
var imageFormat = ".png"
function GetImage() {
    var rn = Math.floor(Math.random() * productImageID.length);
    return imagePath + productImageID[rn] + imageFormat;
}
$('img').one("error", (e) => { $(e.target).attr('src', GetImage()) });
