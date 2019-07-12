$(function () {
    updateCartProducts();

    $(".cartBtn").click(function () {        
        $(".sCart").toggleClass("showCart");
        $.ajax({
            url: '/Shop/Cart'

        })
            .done(function (response) {
                
                $('.sCart').html(response);
            })
            .fail(function () {
                alert("Failed")
            });
    });

    $(document).mouseup(function (e) {
        var popup = $(".sCart");
        if (!$('.cartBtn').is(e.target) && !popup.is(e.target) && popup.has(e.target).length == 0) {
            $(".sCart").removeClass("showCart");
        }
    });
    
});


function updateCartProducts() {
    var cartProducts;
    var existingCookieData = $.cookie('CartProducts');

    if (existingCookieData != undefined && existingCookieData != "" && existingCookieData != null) {
        cartProducts = existingCookieData.split('-');
    }
    else {
        cartProducts = [];
    }

    $(".productCount").html(cartProducts.length);
};




        
