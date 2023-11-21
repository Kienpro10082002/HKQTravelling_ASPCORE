$(document).ready(function () {
    /* banner slide */
    $('.banner__slide').owlCarousel({
        items: 1,
        loop: true,
        margin: 10,
        nav: true,
        dots: false,

        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplay: true,
        autoplayTimeout: 4000,
        autoplaySpeed: 500

    })


    /* my-service slide */

    $('.my-service__list').owlCarousel({
        margin: 10,
        dots: false,
        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplayTimeout: 4000,
        autoplaySpeed: 500,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            600: {
                items: 2,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            1000: {
                items: 4,
                loop: false,
                nav: false,
                mouseDrag: false
            }
        }

    })

    /* featured-post  slide */
    $('.featured-slide').slick({
        infinite: true,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [{
            breakpoint: 2560,
            settings: {
                slidesToShow: 8,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 991,
            settings: {
                slidesToShow: 5,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 767,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 319,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1

            }
        }
        ]


    });


    /* handbook slide */
    $('.handbook__top').slick({
        autoplay: true,
        fade: true,
        autoplaySpeed: 3000,
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        dots: false,
        asNavFor: '.handbook__bottom'
    });
    $('.handbook__bottom').slick({
        autoplay: true,
        autoplaySpeed: 3000,
        slidesToShow: 3,
        slidesToScroll: 1,
        asNavFor: '.handbook__top',
        dots: true,
        focusOnSelect: true,
        responsive: [{
            breakpoint: 2560,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 991,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 767,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 319,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1

            }
        }
        ]
    });


    /* video-box__slide */

    $('.video-box__slide').slick({
        infinite: true,
        dots: true,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [{
            breakpoint: 2560,
            settings: {
                slidesToShow: 5,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 991,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 767,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 319,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1

            }
        }
        ]

    });


    /* widget-img */
    $('.widget-img').slick({
        infinite: true,
        arrows: false,
        autoplay: true,
        autoplaySpeed: 2000,
        slidesToShow: 1,
        slidesToScroll: 1
    });


    /* customer__slide  */

    $('.customer__slide ').slick({
        infinite: true,
        autoplay: true,
        autoplaySpeed: 2000,
        responsive: [{
            breakpoint: 2560,
            settings: {
                slidesToShow: 6,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 991,
            settings: {
                slidesToShow: 4,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 767,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1

            }
        },
        {
            breakpoint: 319,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1

            }
        }
        ]

    });

    /* hot-tour__list */
    $('.hot-tour__mobile').owlCarousel({
        margin: 10,
        dots: false,
        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplayTimeout: 4000,
        autoplaySpeed: 500,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            600: {
                items: 2,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            767: {
                items: 3,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            992: {
                items: 3,
                loop: false,
                nav: false,
                mouseDrag: false
            }
        }

    });

    /*  featured-tour__list */
    $('.featured-tour__mobile').owlCarousel({
        margin: 10,
        dots: false,
        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplayTimeout: 4000,
        autoplaySpeed: 500,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            600: {
                items: 2,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            767: {
                items: 3,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            992: {
                items: 3,
                loop: false,
                nav: false,
                mouseDrag: false
            }
        }
    });

    /* combo__list */
    $('.combo__mobile').owlCarousel({
        margin: 10,
        dots: false,
        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplayTimeout: 4000,
        autoplaySpeed: 500,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            600: {
                items: 2,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            767: {
                items: 2,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            992: {
                items: 3,
                loop: false,
                nav: false,
                mouseDrag: false
            }
        }
    });

    /* image-box__slide */
    $('.image-box__mobile').owlCarousel({
        margin: 10,
        dots: false,
        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplayTimeout: 4000,
        autoplaySpeed: 500,
        responsiveClass: true,
        responsive: {
            0: {
                items: 2,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            600: {
                items: 4,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            767: {
                items: 5,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            992: {
                items: 6,
                loop: false,
                nav: false,
                mouseDrag: false
            }
        }
    });
    /* Active slide when table desktop */
    // $(function() {
    //     let isMobile = window.matchMedia("only screen and (max-width: 1023px)").matches;

    //     if (isMobile) {
    //         $(".list-tour").addClass("owl-carousel owl-theme");
    //         $(".image-box__slide").addClass("owl-carousel owl-theme");


    //     } else {
    //         $(".list-tour").removeClass("owl-carousel owl-theme");
    //         $(".image-box__slide").removeClass("owl-carousel owl-theme");

    //     }
    // });



    // Slide News

    $('.carousel-inner').slick({
        infinite: true,
        arrows: false,
        autoplay: true,
        autoplaySpeed: 2000,
        slidesToShow: 1,
        slidesToScroll: 1,
    });



    /* Slide - detail tour */
    $('.pagetour__slide').slick({
        infinite: true,
        arrows: true,
        autoplay: true,
        autoplaySpeed: 2000,
        slidesToShow: 1,
        slidesToScroll: 1
    });



    /* ------- detail hotel page --------- */

    $('.dhotel__slide').owlCarousel({
        items: 1,
        loop: true,
        nav: true,
        dots: false,
        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplay: true,
        autoplayTimeout: 4000,
        autoplaySpeed: 500

    })


    $('.dhotel__slide-top').owlCarousel({
        items: 1,
        loop: false,
        center: true,
        margin: 10,
        nav: false,
        dots: false,
        URLhashListener: true,
        autoplayHoverPause: true,
        startPosition: 'URLHash'
    });

    $('.dhotel__slide-bottom').owlCarousel({
        loop: true,
        margin: 10,
        nav: true,
        dots: false,
        navText: ['<span class="fas fa-chevron-left fa-2x"></span>', '<span class="fas fa-chevron-right fa-2x"></span>'],
        responsiveClass: true,
        responsive: {
            0: {
                items: 3
            },
            600: {
                items: 4
            },
            1000: {
                items: 4
            }
        }

    })

    $('.dhotel__room-same').owlCarousel({
        margin: 10,
        dots: false,
        navText: ['<img src="./Assets/images/icon/left.svg">', '<img src="./Assets/images/icon/right.svg">'],
        autoplayTimeout: 4000,
        autoplaySpeed: 500,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            600: {
                items: 2,
                loop: true,
                nav: true,
                autoplay: true,
                mouseDrag: true
            },
            1000: {
                items: 2,
                loop: false,
                nav: false,
                mouseDrag: false
            }
        }

    })
})
1