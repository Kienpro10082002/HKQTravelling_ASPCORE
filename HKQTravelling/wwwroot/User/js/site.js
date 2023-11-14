// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Lấy phần tử biểu tượng mũi tên
const arrowElement = document.querySelector(".header__info--tel .fas.fa-angle-down");

// Lưu trữ trạng thái hiện tại của div chứa thông tin điện thoại thứ hai
let isVisible = false;

// Khi biểu tượng mũi tên được nhấp chuột
arrowElement.addEventListener("click", function () {
    // Thay đổi trạng thái hiện tại của div chứa thông tin điện thoại thứ hai
    isVisible = !isVisible;

    // Hiển thị hoặc ẩn div chứa thông tin điện thoại thứ hai
    if (isVisible) {
        document.querySelector(".header__info--hotline").style.display = "block";
    } else {
        document.querySelector(".header__info--hotline").style.display = "none";
    }
});

// Thêm hiệu ứng chuyển đổi
arrowElement.style.transition = "transform 0.2s ease-in-out";