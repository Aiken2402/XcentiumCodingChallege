let slideIndex = 1;

$("#btn1").click(function () {
    if (!isEmptyOrSpaces($("#txt1").val())) {
        var formData = { url: encodeURI($("#txt1").val().trim()) };
        $.ajax({
            url: "/PageContent/GetContent/",
            type: "GET",
            data: formData,
            success: function (data, textStatus, jqXHR) {
                $("#pageContent").html(data);
                slideIndex = 1;
                showSlides(slideIndex);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                //Handle error
            }
        });
    }
    else {
        alert("please enter text");
    }
});

function isEmptyOrSpaces(str) {
    return str === null || str.match(/^ *$/) !== null;
}

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slideIndex - 1].style.display = "block";
}