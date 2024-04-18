$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var t = document.getElementById("qty").value
        if (t != null) {
            quantity = parseInt(t);
        }
        $.ajax({
            url: '/cart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                console.log(rs.Success); // In ra trạng thái của rs.Success
                if (rs.Success) {
                    console.log(rs.Count); // In ra trạng thái của rs.Success
                    $('#ItemCount').html(rs.Count);
                    alert(rs.msg);
                }
            }
        });
    });

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        Update(id, quantity);
    });

    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Bạn có chắc chắn muốn xoá tất cả sản phẩm khỏi giỏ hàng?");
        if (conf == true) {
            DeleteAll();
        }

    });

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Bạn có chắc chắn muốn xoá sản phẩm này khỏi giỏ hàng?");
        if (conf == true) {
            $.ajax({
                url: '/cart/delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $('#ItemCount').html(rs.Count);
                        $('#throw_' + id).remove();
                        LoadCart();
                    }
                }
            });
        }

    });

});

function ShowCount() {
    $.ajax({
        url: '/cart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#ItemCount').html(rs.Count);
        }
    });
}

function DeleteAll() {
    $.ajax({
        url: '/cart/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
                ShowCount();
            }

        }
    });
}

function Update(id, quantity) {
    $.ajax({
        url: '/cart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }

        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/cart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            console.log("LoadCart thanh cong"); // In ra trạng thái của rs.Success
            $('#load_data').html(rs);

        }
    });
}