$(document).ready(function () {

    /* Content appear */
    if ($('body').hasClass('content-appear')) {
        $('body').addClass('content-appearing')
        setTimeout(function () {
            $('body').removeClass('content-appear content-appearing');
        }, 800);
    }

    /* Preloader */
    setTimeout(function () {
        $('.preloader').fadeOut();
    }, 500);

    /* Scroll */
    if (jQuery.browser.mobile == false) {
        function initScroll() {
            $('.custom-scroll').jScrollPane({
                autoReinitialise: true,
                autoReinitialiseDelay: 100
            });
        }

        initScroll();

        $(window).resize(function () {
            initScroll();
        });
    }

    /* Scroll - if mobile */
    if (jQuery.browser.mobile == true) {
        $('.custom-scroll').css('overflow-y', 'scroll');
    }

    /* Switch sidebar to compact */
    if (matchMedia) {
        var mq = window.matchMedia("(min-width: 768px) and (max-width: 991px)");
        mq.addListener(WidthChange);
        WidthChange(mq);
    }

    function WidthChange(mq) {
        if (mq.matches) {
            $('body').addClass('compact-sidebar');
            $('.site-sidebar li.with-sub').find('>ul').slideUp();
        } else {
            $('body').removeClass('compact-sidebar');
            sidebarIfActive();
        }
    }

    /* Fullscreen */
    $('.toggle-fullscreen').click(function () {
        $(document).toggleFullScreen();
    });

    /* Sidebar - on click */
    $('.site-sidebar li.with-sub > a').click(function () {
        if (!$('body').hasClass('compact-sidebar')) {
            if ($(this).parent().hasClass('active')) {
                $(this).parent().removeClass('active');
                $(this).parent().find('>ul').slideUp();
            } else {
                if (!$(this).parent().parent().closest('.with-sub').length) {
                    $('.site-sidebar li.with-sub').removeClass('active').find('>ul').slideUp();
                }
                $(this).parent().addClass('active');
                $(this).parent().find('>ul').slideDown();
            }
        }
    });

    /* Sidebar - if active */
    function sidebarIfActive() {
        $('.site-sidebar ul > li:not(.with-sub)').removeClass('active');
        var url = window.location;
        var element = $('.site-sidebar ul > li > a').filter(function () {
            return this.href == url || url.href.indexOf(this.href) == 0;
        });
        element.parent().addClass('active');

        $('.site-sidebar li.with-sub').removeClass('active').find('>ul').hide();
        var url = window.location;
        var element = $('.site-sidebar ul li ul li a').filter(function () {
            return this.href == url || url.href.indexOf(this.href) == 0;
        });
        element.parent().addClass('active');
        element.parent().parent().parent().addClass('active');

        if (!$('body').hasClass('compact-sidebar')) {
            element.parent().parent().slideDown();
        }
    }

    sidebarIfActive();

    /* Sidebar - show and hide */
    $('.site-header .sidebar-toggle-first').click(function () {
        if ($('body').hasClass('site-sidebar-opened')) {
            $('body').removeClass('site-sidebar-opened');
            if (jQuery.browser.mobile == false) {
                $('html').css('overflow', 'auto');
            }
        } else {
            $('body').addClass('site-sidebar-opened');
            if (jQuery.browser.mobile == false) {
                $('html').css('overflow', 'hidden');
            }
        }
    });

    $('.site-header .sidebar-toggle-second').click(function () {
        var compact = 'compact-sidebar';

        if ($('body').hasClass(compact)) {
            $('body').removeClass(compact);
            sidebarIfActive();
        } else {
            $('body').addClass(compact);
            $('.site-sidebar li.with-sub').find('>ul').slideUp();
        }
    });

    /* Sidebar - overlay */
    $('.site-overlay').click(function () {
        $('.site-header .sidebar-toggle-first').removeClass('active');
        $('body').removeClass('site-sidebar-opened');
        if (jQuery.browser.mobile == false) {
            $('html').css('overflow', 'auto');
        }
    });

    /* Sidebar second - toggle */
    $('.toggle-button-second').click(function () {
        $('.template-options').toggle();
        $('.template-options').removeClass('opened');
        $(this).toggleClass('active');
        $('.site-sidebar-second').toggleClass('opened');
    });

    /* Template options - toggle */
    $('.template-options .to-toggle').click(function () {
        $('.template-options').toggleClass('opened');
    });

    /* Chat */
    $('.sidebar-chat a, .sidebar-chat-window a').click(function () {
        $('.sidebar-chat').toggle();
        $('.sidebar-chat-window').toggle();
    });

    /* Switchery */
    $('.js-switch').each(function () {
        new Switchery($(this)[0], $(this).data());
    });

    /* Template options */
    $('.template-options input:checkbox').change(function () {

        if ($('body').hasClass('fixed-footer')) {
            $('body').removeClass('fixed-footer');
        }

        var setting = $(this).attr('name');

        if ($('body').hasClass(setting)) {
            $('body').removeClass(setting);
            if (setting == 'compact-sidebar') {
                sidebarIfActive();
            }
            if (setting == 'fixed-header') {
                $('body').removeClass('fixed-sidebar');
                $('.template-options input[name="fixed-sidebar"]').prop('checked', false);
            }
            if (setting == 'boxed-wrapper') {
                $('.template-options input[name="fixed-header"]').parent().parent().removeClass('disabled');
                $('.template-options input[name="fixed-sidebar"]').parent().parent().removeClass('disabled');
            }
        } else {
            $('body').addClass(setting);
            if (setting == 'compact-sidebar') {
                $('.site-sidebar li.with-sub').find('>ul').slideUp();
            }
            if (setting == 'fixed-sidebar') {
                $('body').addClass('fixed-header');
                $('.template-options input[name="fixed-header"]').prop('checked', true);
            }
            if (setting == 'boxed-wrapper') {
                $('body').removeClass('fixed-header');
                $('.template-options input[name="fixed-header"]').prop('checked', false);
                $('.template-options input[name="fixed-header"]').parent().parent().addClass('disabled');
                $('body').removeClass('fixed-sidebar');
                $('.template-options input[name="fixed-sidebar"]').prop('checked', false);
                $('.template-options input[name="fixed-sidebar"]').parent().parent().addClass('disabled');
                $('body').removeClass('static');
                $('.template-options input[name="static"]').prop('checked', false);
            }
            if (setting == 'static') {
                $('body').removeClass('fixed-header');
                $('.template-options input[name="fixed-header"]').prop('checked', false);
                $('.template-options input[name="fixed-header"]').parent().parent().removeClass('disabled');
                $('body').removeClass('fixed-sidebar');
                $('.template-options input[name="fixed-sidebar"]').prop('checked', false);
                $('.template-options input[name="fixed-sidebar"]').parent().parent().removeClass('disabled');
                $('body').removeClass('boxed-wrapper');
                $('.template-options input[name="boxed-wrapper"]').prop('checked', false);
            }
        }

    });

    $('.template-options input:radio').change(function () {
        var setting = $(this).val();

        $('body').removeClass(function (index, css) {
            return (css.match(/(^|\s)skin-\S+/g) || []).join(' ');
        });

        $('body').addClass(setting);

        if (setting == 'skin-default' || setting == 'skin-2' || setting == 'skin-3') {
            $('.site-header .navbar').removeClass('navbar-dark').addClass('navbar-light');
        } else {
            $('.site-header .navbar').removeClass('navbar-light').addClass('navbar-dark');
        }

        if (setting == 'skin-3' || setting == 'skin-4') {
            $('.site-header .navbar .navbar-left .toggle-button.dark').removeClass('dark').addClass('light');
            $('.site-header .navbar .navbar-left .toggle-button-second.dark').removeClass('dark').addClass('light');
        } else {
            $('.site-header .navbar .navbar-left .toggle-button.light').removeClass('light').addClass('dark');
            $('.site-header .navbar .navbar-left .toggle-button-second.light').removeClass('light').addClass('dark');
        }

        if (setting == 'skin-default' || setting == 'skin-2' || setting == 'skin-3') {
            $('.site-header .navbar .navbar-right .toggle-button.dark').removeClass('dark').addClass('light');
            $('.site-header .navbar .navbar-right .toggle-button-second.dark').removeClass('dark').addClass('light');
        } else {
            $('.site-header .navbar .navbar-right .toggle-button.light').removeClass('light').addClass('dark');
            $('.site-header .navbar .navbar-right .toggle-button-second.light').removeClass('light').addClass('dark');
        }

        if (setting == 'skin-default' || setting == 'skin-2' || setting == 'skin-6') {
            $('.site-sidebar .custom-scroll').removeClass('custom-scroll-dark').addClass('custom-scroll-light');
            $('.site-sidebar .progress-widget').removeClass('progress-widget-dark').addClass('progress-widget-light');
        } else {
            $('.site-sidebar .custom-scroll').removeClass('custom-scroll-light').addClass('custom-scroll-dark');
            $('.site-sidebar .progress-widget').removeClass('progress-widget-light').addClass('progress-widget-dark');
        }
    });

    /* Hide on outside click */
    $(document).mouseup(function (e) {
        var container = $('.template-options, .site-sidebar-second, .toggle-button-second');

        if (!container.is(e.target) && container.has(e.target).length === 0) {
            container.removeClass('opened');
            $('.template-options').show();
            $('.toggle-button-second').removeClass('active');
        }
    });

    /*  Tooltip */
    $('[data-toggle="tooltip"]').tooltip();

    /*  Popover */
    $('[data-toggle="popover"]').popover();



    $('.depended-select').each(function () {
        var dependedElement = $(this);
        var dependedSelect = $(this);
        var dependedElementSelector = dependedSelect.attr('dependedElement');
        var dataUrl = $(this).attr('dataUrl');
        $(dependedElementSelector).change(function () {
            dependedElement.find('Option').remove();
            var selectedValue = $(this).val();
            var requestBody = { Id:selectedValue };
            $.ajax({
                url: dataUrl,
                method: 'POST',
                contentType: "application/json",
                data: JSON.stringify(requestBody),
                success: function (response) {
                    $.each(response, function (index, item) {
                        dependedElement.append($('<option></option>').val(item.Id).html(item.Name));
                    });
                },
                error: function () {
                    alert("Hata");
                }
            })
        })
    })

    $(".ContactTypeSelect").change(function () {

        var select = $(this).val();
        var dataUrl = "/api/common/getPickListEvent";
        var dependedElements = $("#ContactTypeId");
        dependedElements.find('Option').remove();
        if (select == 22) {
            dataUrl = "/api/common/getPickListEvent";
            $(".contacttypeSub").css("visibility", "visible");
            //$("#ContactTypeId").prev().text("Etkinlik Türü")
            $("#lbltemas").css("display", "block");
            $("#lblarac").css("display", "none");
        }
        else if (select == 23) {
            dataUrl = "/api/common/getVehicle";
            $(".contacttypeSub").css("visibility", "hidden");
            //$("#ContactTypeId").prev().text("Arac")
            $("#lbltemas").css("display", "none");
            $("#lblarac").css("display", "block");
        }
        $.ajax({
            url: dataUrl,
            method: 'POST',
            success: function (response) {
                dependedElements.append($('<option></option>').val("").html("Seciniz"));
                $.each(response, function (index, item) {
                    if (select == 22) {
                        dependedElements.append($('<option></option>').val(item.Id).html(item.Name));
                    }
                    else if (select == 23) {
                        dependedElements.append($('<option></option>').val(item.Id).html(item.Model + " - " + item.Serial));

                    }
                });
            },
            error: function () {
                alert("Hata");
            }
        })
    });

    $(".countrySelect").change(function () {
        var select = $(this).val();
        var requestBody = { Id: select };
        var dependedElements = $("#DistrictId");
        dependedElements.find('Option').remove();
        $.ajax({
            url: "/api/common/getdistrictlist",
            method: 'POST',
            contentType: "application/json",
            data: JSON.stringify(requestBody),
            success: function (response) {
                $.each(response, function (index, item) {
                    dependedElements.append($('<option></option>').val(item.Id).html(item.Name));
                });
            },
            error: function () {
                alert("Hata");
            }
        })
    });


    $(".countrySelect").each(function () {
        var districtSelect = $(".hiddenDistrict").val();
        var select = $(this).val();
        var requestBody = { Id: select };
        var dependedElements = $("#DistrictId");
        dependedElements.find('Option').remove();
        $.ajax({
            url: "/api/common/getdistrictlist",
            method: 'POST',
            contentType: "application/json",
            data: JSON.stringify(requestBody),
            success: function (response) {
                $.each(response, function (index, item) {
                    if (item.Id == districtSelect)
                    {
                        dependedElements.append($('<option selected ></option>').val(item.Id).html(item.Name));
                    }
                    else {
                    dependedElements.append($('<option></option>').val(item.Id).html(item.Name));

                    }
                });
            },
            error: function () {
                alert("Hata");
            }
        })
    });
   

    //Temas türü araç tahsisiyse alt kategori gösterilmesin
    if ($("#ContactTypeSelectId").val() == 23) {
        $(".contacttypeSub").css("visibility", "hidden");
    }

    $('.btn-file-upload').click(function () {
        var elemetData = $(this).data();
        var url = "/upload/index?elementId=" + elemetData.elementid;

        window.open(url, "_blank", "toolbar=yes,scrollbars=yes,resizable=yes,top=300,left=-800,width=400,height=200");

        return false;
    })

    $(".btnBlockOff").click(function () {
        $(".btnBlockOn").addClass("btnBlockOff");
        $(this).addClass("btnBlockOn");
    })

    $("#Path1Control1").hover(function () {
        jQuery(this).children("img").css("opacity", "0.3");
        jQuery(this).children("a:nth-child(1)").css("visibility", "visible");
        jQuery(this).children(".imgLarge").css("visibility", "visible");
    });

    $("#Path1Control2").hover(function () {
        jQuery(this).children("img").css("opacity", "0.3");
        jQuery(this).children("a:nth-child(1)").css("visibility", "visible");
        jQuery(this).children(".imgLarge").css("visibility", "visible");
    });
    $("#Path1Control3").hover(function () {
        jQuery(this).children("img").css("opacity", "0.3");
        jQuery(this).children("a:nth-child(1)").css("visibility", "visible");
        jQuery(this).children(".imgLarge").css("visibility", "visible");
    });
    $("#Path1Control4").hover(function () {
        jQuery(this).children("img").css("opacity", "0.3");
        jQuery(this).children("a:nth-child(1)").css("visibility", "visible");
        jQuery(this).children(".imgLarge").css("visibility", "visible");
    });

    $("#Path1Control1").mouseleave(function () {
        jQuery(this).children("img").css("opacity", "1");
        jQuery(this).children("a:nth-child(1)").css("visibility", "hidden");
        jQuery(this).children(".imgLarge").css("visibility", "hidden");
    });

    $("#Path1Control2").mouseleave(function () {
        jQuery(this).children("img").css("opacity", "1");
        jQuery(this).children("a:nth-child(1)").css("visibility", "hidden");
        jQuery(this).children(".imgLarge").css("visibility", "hidden");

    });

    $("#Path1Control3").mouseleave(function () {
        jQuery(this).children("img").css("opacity", "1");
        jQuery(this).children("a:nth-child(1)").css("visibility", "hidden");
        jQuery(this).children(".imgLarge").css("visibility", "hidden");

    });

    $("#Path1Control4").mouseleave(function () {
        jQuery(this).children("img").css("opacity", "1");
        jQuery(this).children("a:nth-child(1)").css("visibility", "hidden");
        jQuery(this).children(".imgLarge").css("visibility", "hidden");

    });

    $(".avatar-img").hover(function () {
        jQuery(this).children("img").css("opacity", "0.3");
        jQuery(this).children("a:nth-child(2)").css("visibility", "visible");
        jQuery(this).children(".imgLarge").css("visibility", "visible");
    });

    $(".avatar-img").mouseleave(function() {
        jQuery(this).children("img").css("opacity", "1");
        jQuery(this).children("a:nth-child(2)").css("visibility", "hidden");
        jQuery(this).children(".imgLarge").css("visibility", "hidden");
    });

    imgControl('Path1Control1');
    imgControl('Path1Control2');
    imgControl('Path1Control3');
    imgControl('Path1Control4');
    function imgControl(path) {
        if ($('#' + path).children(':nth-child(3)').attr('href') == '/upload/default.png') {
            $('#' + path).children(':first-child').text('Gorsel Ekle');
        }
        else {
            $('#' + path).children(':first-child').text('Degistir');
        }
    }
    $(document).on('click', '.backLink', function () {
        history.go(-1);
    });
    $('.orderby-btn').click(function () {
        var d = $(this).data();

        var myUri = URI(window.location);

        var orderType = myUri.query(true)["OrderType"];

        if (orderType == "false") {
            orderType = "true";
        }
        else {
            orderType = "false";
        }

        myUri.setSearch("OrderByColumn", d.orderbycolumn);
        myUri.setSearch("OrderType", orderType);

        window.location = myUri.toString();

        return false;
    });

    $('.block-control-true').click(function () {
        alert("block");
        $("input[name='Block']").val('True');
       
    });
    $('.block-control-false').click(function () {
        alert("block");
        $("input[name='Block']").val('False');
    });

    $('#SelectedValues').multipleSelect();

    $('.mailduplicate').focusout(function () {
        var Email = $(this).val();
        var requestBody = { Email: Email };
        $.ajax({
            url: "/api/common/EmailDuplicate",
            method: 'POST',
            contentType: "application/json",
            data: JSON.stringify(requestBody),
            success: function (response) {
                if (response == true)
                {
                    document.getElementById("btnkaydet").disabled = true;
                    $('#errorMail').text('Sistemimizde  ' + Email + '  zaten mevcut!');
                }
                else {
                    document.getElementById("btnkaydet").disabled = false;
                    $('#errorMail').text('');
                }
            },
            error: function () {
                console.log('Hata');
            }
        })

    });
});