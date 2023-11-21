$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    /* date picker - home-search */
    $(function () {

        $('input[name="departure"],input[name="returnDate"]').daterangepicker({
            singleDatePicker: true,
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            }
        });

        $('input[name="departure"],input[name="returnDate"]').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('input[name="departure"],input[name="returnDate"]').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });

    });
    /* fixed menu */
    $(window).scroll(function () {
        var hight_top = $(".header__top").height();


        if ($(window).scrollTop() > (hight_top + 15) && $(window).width() > 991) {
            $(".header__bottom").addClass("sticky");
        } else {
            $(".header__bottom").removeClass("sticky");
        }
    });


    /*   */

    $(function isMobile() {
        $(window).resize(myResizeFunction).trigger('resize');
    });

    function myResizeFunction() {
        if ($(document).width() < 992) {
            var head_height = $(".header").height();
            $(window).scroll(function () {
                var scroll_pos = $(window).scrollTop();
                if (scroll_pos > head_height) {
                    $(".header").addClass("fixed")
                } else {
                    $(".header").removeClass("fixed")
                }
            });
        }
        else { }
    }

    /* toggle menu-mobile */

    $(".toogle-menu").click(function (e) {
        $(".header__bottom").toggleClass("active");
        $(".overlay-menu ").toggleClass("active");

    });
    $(".overlay-menu").click(function (e) {
        $(".header__bottom").removeClass("active");
        $(".overlay-menu ").removeClass("active");
    });


    /* Form validation */
    $("#form-getsale").validate({

        rules: {
            "name": {
                required: true,
                minlength: 4
            },
            "mail": {
                required: true,
                email: true

            },
            "phone-number": {
                required: true,
                number: true,
                minlength: 10

            }
        },
        messages: {
            "name": {
                required: "ThĂ´ng tin nĂ y lĂ  báº¯t buá»™c",
                minlength: "Há» TĂªn pháº£i lá»›n hÆ¡n 4 kĂ½ tá»±"
            },
            "mail": {
                required: "Email lĂ  báº¯t buá»™c",
                email: "Email khĂ´ng Ä‘Ăºng Ä‘á»‹nh dáº¡ng"
            },
            "phone-number": {
                required: "Sá»‘ Ä‘iá»‡n thoáº¡i lĂ  báº¯t buá»™c",
                minlength: "Sá»‘ Ä‘iá»‡n thoáº¡i pháº£i cĂ³ 10 chá»¯ sá»‘",
                number: "Sá»‘ Ä‘iá»‡n thoáº¡i pháº£i lĂ  dáº¡ng sá»‘"
            }
        }
    });


    /* rate star- list tour */
    $(function () {
        $(".ratestar").rating();
    });
    $("#review").rating({
        "stars": 5,
        "click": function (e) {
            console.log(e); // {stars: 3, event: E.Event}
            alert(e.stars); // 3
        }
    });


    /* detail tour */
    $(".tour__card-link").click(function (e) {
        e.preventDefault();
        $(this).find("img").toggleClass("rotate");
    });




    $("#rating-from").validate({


        rules: {
            "name": {
                required: true,
                minlength: 4
            },
            "mail": {
                required: true,
                email: true

            },
            "phone-number": {
                required: true,
                number: true,
                minlength: 10

            },
            "head-rate": {
                required: true
            },
            "rate-text": {
                required: true


            }
        },
        messages: {
            "name": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c",
                minlength: "Há» TĂªn pháº£i lá»›n hÆ¡n 4 kĂ½ tá»±"
            },
            "mail": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c",
                email: "Email khĂ´ng Ä‘Ăºng Ä‘á»‹nh dáº¡ng"
            },
            "phone-number": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c",
                minlength: "Sá»‘ Ä‘iá»‡n thoáº¡i pháº£i cĂ³ 10 chá»¯ sá»‘",
                number: "Sá»‘ Ä‘iá»‡n thoáº¡i pháº£i lĂ  dáº¡ng sá»‘"
            },
            "head-rate": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c"

            },
            "rate-text": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c"

            }
        }
    });

    $("#leave-info").validate({


        rules: {
            "name": {
                required: true,
                minlength: 4
            },
            "email-phone": {
                required: true,


            }
        },
        messages: {
            "name": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c",
                minlength: "Há» TĂªn pháº£i lá»›n hÆ¡n 4 kĂ½ tá»±"
            },
            "email-phone": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c",

            }
        }
    });
    $("#leave-info2").validate({


        rules: {
            "name": {
                required: true,
                minlength: 4
            },
            "email-phone": {
                required: true,


            }
        },
        messages: {
            "name": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c",
                minlength: "Há» TĂªn pháº£i lá»›n hÆ¡n 4 kĂ½ tá»±"
            },
            "email-phone": {
                required: "Má»¥c nĂ y lĂ  báº¯t buá»™c",

            }
        }
    });



    /* ----- detail hotel ----- */
    $(function () {
        $('input[name="date_start"]').daterangepicker({
            singleDatePicker: true,
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            }
        });
        $('input[name="date_start"]').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('input[name="date_start"]').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });
    });


    $(function () {
        $('input[name="date_end"]').daterangepicker({
            singleDatePicker: true,
            autoUpdateInput: false,
            locale: {
                cancelLabel: 'Clear'
            }
        });
        $('input[name="date_end"]').on('apply.daterangepicker', function (ev, picker) {
            $(this).val(picker.startDate.format('MM/DD/YYYY'));
        });

        $('input[name="date_end"]').on('cancel.daterangepicker', function (ev, picker) {
            $(this).val('');
        });
    });


    /* ----------- page contact -------- */
    $(".contact__right").validate({
        rules: {
            name: "required",
            phone: {
                required: true,
                minlength: 10,
                digits: true
            },
            guys: {
                required: true,
                digits: true
            }
        },
        messages: {
            name: "Vui lĂ²ng nháº­p tĂªn",
            phone: {
                required: "Vui lĂ²ng nháº­p sá»‘ Ä‘iá»‡n thoáº¡i",
                minlength: "Sá»‘ Ä‘iá»‡n thoáº¡i tá»‘i thiá»ƒu 10 chá»¯ sá»‘",
                digits: "Sá»‘ Ä‘iá»‡n thoáº¡i sai Ä‘á»‹nh dáº¡ng"
            },
            guys: {
                required: "Vui lĂ²ng nháº­p sá»‘ ngÆ°á»i",
                digits: "Pháº£i lĂ  sá»‘"
            }
        }
    });



    /*------------ Search page ------------ */
    $(".form").validate({
        rules: {
            name: "required",
            phone: {
                required: true,
                minlength: 10,
                digits: true,
            },
        },
        messages: {
            name: "Vui lĂ²ng nháº­p tĂªn",
            phone: {
                required: "Vui lĂ²ng nháº­p tĂªn hoáº·c sá»‘ Ä‘iá»‡n thoáº¡i",
                minlength: "Sá»‘ Ä‘iá»‡n thoáº¡i tá»‘i thiá»ƒu 10 chá»¯ sá»‘",
                digits: "Sá»‘ Ä‘iá»‡n thoáº¡i sai Ä‘á»‹nh dáº¡ng",
            },
        },
    });


    $(".find__button--another").click(function () {
        $(".home-search").css("display", "block");
        $(".find__button--another").css("display", "none");
        $(".find__button--filter").css("display", "none");
        $(".find").css("margin-top", "0");
    });

    $("#close-home-search").click(function () {
        $(".home-search").css("display", "none");
        $(".find__button--another").css("display", "block");
        $(".find__button--filter").css("display", "block");
        $(".find").css("margin-top", "15px");
    });

    // SHOW / HIDE mobile filter
    $(".click-mobile").click(function () {
        // $(".mobile").css({ right: "0", display: "block" });
        $(".find__button--another").css("visibility", "hidden");
        $(".mobile").css("right", "0");
        $(".click-mobile").css("display", "none");
    });

    $(".close-mobile").click(function () {
        // $(".mobile").css({ right: "-290px", display: "none" });
        $(".mobile").css("right", "-300px");
        $(".click-mobile").css("display", "block");
        $(".find__button--another").css("visibility", "inherit");
    });


    /* ----------- rent car -------- */
    $(".form").validate({
        rules: {
            name: "required",
            phone: {
                required: true,
                minlength: 10,
                digits: true
            },
            email: {
                required: true,
                email: true
            },
            place: {
                required: true,
                minlength: 5
            },
            drop: "required",
            car: "required",
            time: {
                required: true,
                digits: true
            }
        },
        messages: {
            name: "Vui lĂ²ng nháº­p tĂªn",
            phone: {
                required: "Vui lĂ²ng nháº­p sá»‘ Ä‘iá»‡n thoáº¡i",
                minlength: "Sá»‘ Ä‘iá»‡n thoáº¡i tá»‘i thiá»ƒu 10 chá»¯ sá»‘",
                digits: "Sá»‘ Ä‘iá»‡n thoáº¡i sai Ä‘á»‹nh dáº¡ng"
            },
            email: {
                required: "Vui lĂ²ng nháº­p email",
                email: "Email khĂ´ng Ä‘Ăºng Ä‘á»‹nh dáº¡ng"
            },
            place: {
                required: "Vui lĂ²ng nháº­p Ä‘á»‹a chá»‰",
                minlength: "Äá»‹a chá»‰ quĂ¡ ngáº¯n"
            },
            drop: "Vui lĂ²ng chá»n loáº¡i xe",
            car: "Vui lĂ²ng chá»n hĂ£ng xe",
            time: {
                required: "Vui lĂ²ng nháº­p sá»‘ ngĂ y",
                digits: "Pháº£i lĂ  sá»‘"
            }
        }
    });
    /* ----- book-tour page ------*/

    /* custom input number */



    var input_tour = $('#select-number-tour'),
        btnUp = $('.quantity-up-tour'),
        btnDown = $('.quantity-down-tour'),
        min = input_tour.attr('min'),
        max = input_tour.attr('max');

    btnUp.click(function () {
        var oldValue = parseFloat(input_tour.val());
        if (oldValue >= max) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue + 1;
        }
        $('#select-number-tour').val(newVal);
        $('#select-number-tour').trigger('change');
    });

    btnDown.click(function () {
        var oldValue = parseFloat(input_tour.val());
        if (oldValue <= min) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue - 1;
        }
        $('#select-number-tour').val(newVal);
        $('#select-number-tour').trigger('change');
    });

    var input_car = $('#select-number-car'),
        btnUp = $('.quantity-up-car'),
        btnDown = $('.quantity-down-car'),
        min = input_car.attr('min'),
        max = input_car.attr('max');

    btnUp.click(function () {
        var oldValue = parseFloat(input_car.val());
        if (oldValue >= max) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue + 1;
        }
        $('#select-number-car').val(newVal);
        $('#select-number-car').trigger('change');
    });

    btnDown.click(function () {
        var oldValue = parseFloat(input_car.val());
        if (oldValue <= min) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue - 1;
        }
        $('#select-number-car').val(newVal);
        $('#select-number-car').trigger('change');
    });

    var input_yacht = $('#select-number-yacht'),
        btnUp = $('.quantity-up-yacht'),
        btnDown = $('.quantity-down-yacht'),
        min = input_yacht.attr('min'),
        max = input_yacht.attr('max');

    btnUp.click(function () {
        var oldValue = parseFloat(input_yacht.val());
        if (oldValue >= max) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue + 1;
        }
        $('#select-number-yacht').val(newVal);
        $('#select-number-yacht').trigger('change');
    });

    btnDown.click(function () {
        var oldValue = parseFloat(input_yacht.val());
        if (oldValue <= min) {
            var newVal = oldValue;
        } else {
            var newVal = oldValue - 1;
        }
        $('#select-number-yacht').val(newVal);
        $('#select-number-yacht').trigger('change');
    });

    /* Sync price when change input number */

    var price_tour = parseInt($("#tour-price").text())
    var number_tour = parseInt(input_tour.val());
    var price_car = parseInt($('#car-price').text())
    var number_car = parseInt(input_car.val());
    var price_yacht = parseInt($("#yacht-price").text())
    var number_yacht = parseInt(input_yacht.val());

    $('.tour-quantity').click(function (e) {
        e.preventDefault();

        number_tour = parseInt(input_tour.val());
        $(".tour-price-add").text(price_tour * number_tour);

        var total = price_tour * number_tour + price_car * number_car + price_yacht * number_yacht;
        $('.price-total').text(total);
        $('.price-product').text(total);

        format_currency();
    });

    $('.car-quantity').click(function (e) {
        e.preventDefault();

        number_car = parseInt(input_car.val());
        $('.car-price-add').text(price_car * number_car);

        var total = price_tour * number_tour + price_car * number_car + price_yacht * number_yacht;
        $('.price-product').text(total)
        $('.price-total').text(total)
        format_currency();
    });

    $('.yacht-quantity').click(function (e) {
        e.preventDefault();

        number_yacht = parseInt(input_yacht.val());
        $('.yacht-price-add').text(price_yacht * number_yacht);

        var total = price_tour * number_tour + price_car * number_car + price_yacht * number_yacht;
        $('.price-product').text(total)
        $('.price-total').text(total)
        format_currency();
    });

    function format_currency() {
        $('[data-format="currency"]').each(function (index, element) {
            var price = $(this).text().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
            $(this).text(price);
        });
    }

    format_currency();


    /* Sync price when change input number */

    // $('.quantity').click(function (e) {
    //     e.preventDefault();
    //     var price_product = $(".price-defaut").text()

    //     var number_tour = input.val();
    //     $(".price-add").text(price_product * number_tour);
    // });




    /* toggle  cart-detail__bar */
    $(".cart-arrow").click(function () {
        $(".cart-detail__bar").toggleClass("active");
        $(this).find("img").toggleClass("rotate")
    })


    /* cart-close */
    $(".cart-close").click(function () {
        {
            $(this).parents('.card-detail-wp').css("display", "none");
            $(".cart-detail__bar").css("display", "none");
            $(".card-empty").css("display", "block");
        }
    })

    /* toggle  form-bill */
    $(".select-bill input").click(function () {
        $(".form-bill").toggle();
    });


    /* show hide method pay  */
    $("[name='payment']").change(function () {
        var pay_direct = $(".no-online");
        var paybar_precent = $(".paybar-precent");
        if ($(this).val() == "online") {
            pay_direct.css("display", "none");
            paybar_precent.css("display", "block");

        } else {
            pay_direct.css("display", "block");
            paybar_precent.css("display", "none");
        }

    })

    /* toggle bankpay-box */

    $("[name='MethodPayment']").change(function () {

        var bankpay_box = $(".bankpay-box")
        if ($(this).val() == 0) {
            bankpay_box.css("display", "block");
        } else {
            bankpay_box.css("display", "none");
        }
    })
});
1
Ask AI to edit or generate...
