$(document).ready(function () {
    // init form
    bs_input_file();

    $('.select2').select2();

    //$('.count').prop('disabled', true);
    //$(document).on('click', '.plus', function () {
    //    $('.count').val(parseInt($('.count').val()) + 1);
    //});
    //$(document).on('click', '.minus', function () {
    //    $('.count').val(parseInt($('.count').val()) - 1);
    //    if ($('.count').val() == 0) {
    //        $('.count').val(1);
    //    }
    //});

    $('#datepicker').datepicker({
        autoclose: true
    })
    GetCusType();
    GetCustomerType();
    GetPhoneType();
    GetHouseType();
    GetDistrict();
    // end init
});

function bs_input_file() {
    $(".input-file").before(
        function () {
            if (!$(this).prev().hasClass('input-ghost')) {
                var element = $("<input type='file' class='input-ghost' style='visibility:hidden; height:0'>");
                element.attr("name", $(this).attr("name"));
                element.change(function () {
                    element.next(element).find('input').val((element.val()).split('\\').pop());
                });
                $(this).find('input').css("cursor", "pointer");
                $(this).find('input').mousedown(function () {
                    $(this).parents('.input-file').prev().click();
                    return false;
                });
                return element;
            }
        }
    );
}

function plus(idElement) {
    $('#' + idElement).val(parseInt($('#' + idElement).val()) + 1);
}

function minus(idElement) {
    $('#' + idElement).val(parseInt($('#' + idElement).val()) - 1);
    if ($('#' + idElement).val() == 0) {
        $('#' + idElement).val(1);
    }
}

$("#drLoaidichvu").on('change', function (e) {
    var items = $(this).val();
    if (items.indexOf("3") != -1) {
        if (items != "3") {
            showalert("Dịch vụ FPT Play chỉ bán riêng", 0);
            return;
        }
    }
    else if (items.indexOf("4") != -1) {
        if (items != "4") {
            showalert("Dịch vụ FPT Camera chỉ bán riêng", 0);
            return;
        }
    }
    else if (items == "5") {
        showalert("Thiết bị không bán riêng", 0);
        return;
    }

    $("#Internet").hide();
    $("#IPTV").hide();
    $("#FPTPlay").hide();
    $("#FPTCamera").hide();
    $("#ThietBi").hide();

    if (items.indexOf("1") != -1) {
        $("#Internet").show();
        $('#drGoiDichVu').removeAttr('disabled');
    }
    if (items.indexOf("2") != -1) {
        $("#IPTV").show();
        $('#drGoiDichVu').removeAttr('disabled');
        if (items == "2") {
            $('#drGoiDichVu').empty().trigger("change");
            $('#drGoiDichVu').append('<option value="1">FTTH - TV Gold</option>');
            $('#drGoiDichVu').attr('disabled', 'disabled');
        }
    }
    if (items.indexOf("3") != -1) {
        $("#FPTPlay").show();
        $('#drGoiDichVu').empty().trigger("change");
        $('#drGoiDichVu').append('<option value="1">FPT Play</option>');
        $('#drGoiDichVu').attr('disabled', 'disabled');
    }
    if (items.indexOf("4") != -1) {
        $("#FPTCamera").show();
        $('#drGoiDichVu').empty().trigger("change");
        $('#drGoiDichVu').append('<option value="1">FPT Camera</option>');
        $('#drGoiDichVu').attr('disabled', 'disabled');
    }
    if (items.indexOf("5") != -1) {
        $("#ThietBi").show();
        $('#drGoiDichVu').removeAttr('disabled');
    }
});

$('#drHouseType').on('select2:select', function (e) {
    var val = $(this).val();
    if (val == 2) {
        $('#typeHouse_chungcu').show();
        $('#typeHouse_nhapho').hide();
        $('#typeHouse_khongdiachi').hide();
        GetNameVilla();
    }
    else if (val == 3) {
        $('#typeHouse_chungcu').hide();
        $('#typeHouse_nhapho').show();
        $('#typeHouse_khongdiachi').show();
        GetHouseCoordinate();
    }
    else {
        $('#typeHouse_chungcu').hide();
        $('#typeHouse_nhapho').show();
        $('#typeHouse_khongdiachi').hide();
    }
});

$('#drDoiTuongKH').on('select2:select', function (e) {
    var val = $(this).val();
    if (val == 2) {
        $("#ISP").show();
    }
    else {
        $("#ISP").hide();
    }
});

$('#drFPTBox_ChonBox').on('change', function (e) {

    var items = $(this).select2('data')
    var template = [
        '<div>',
        '<div class="form-group">',
        '<label for="inputEmail3" class="col-md-3 control-label">Thiết bị</label>',
        '<div class="col-md-5">',
        '<input type="text" class="form-control" id="inputEmail3" value="{{name}}" disabled="disabled">',
        '</div>',
        '</div>',

        '<div class="form-group">',
        '<label class="col-md-3 control-label">Số lượng</label>',
        '<div class="col-md-5">',
        '<div class="qty mt-5">',
        '<span class="minus bg-dark">-</span>',
        '<input type="number" class="count" name="qty" value="1">',
        '<span class="plus bg-dark">+</span>',
        '</div>',
        '</div>',
        '</div>',

        '<div class="form-group">',
        '<label class="col-md-3 control-label">CLKM</label>',
        '<div class="col-md-5">',
        '<select class="drFPTBox_KhuyenMai form-control select2 select2-hidden-accessible" style="width: 100%;" tabindex="-1" aria-hidden="true">',
        '<option selected="selected">Truyền hình FPT</option>',
        '</select>',
        '</div>',
        '</div>',
        '<hr>',
        '</div>',
    ].join("\n");

    var html = '';
    $.each(items, function (key, value) {
        var data = {
            name: value.text,
        }
        var htmlItem = Mustache.render(template, data);
        html += htmlItem;
    });
    $("#FPTBox_listBox").html(html);
    $(".drFPTBox_KhuyenMai").select2({
        width: "100%"
    });
});

$('#drFPTCamera_ChonThietBi').on('change', function (e) {
    var items = $(this).select2('data')
    var template = [
        '<div>',
        '<div class="form-group">',
        '<label for="inputEmail3" class="col-md-3 control-label">Thiết bị</label>',
        '<div class="col-md-5">',
        '<input type="text" class="form-control" value="{{name}}" disabled="disabled" id="inputEmail3" placeholder="[Họ và tên]">',
        '</div>',
        '</div>',
        '<div class="form-group">',
        '<label class="col-md-3 control-label">Tên gói</label>',
        '<div class="col-md-5">',
        '<select class="drFPTCamera_KhuyenMai form-control select2 select2-hidden-accessible" style="width: 100%;" tabindex="-1" aria-hidden="true">',
        '<option value="1" selected="selected">Truyền hình FPT</option>',
        '</select>',
        '</div>',
        '</div>',
        '<div class="form-group">',
        '<label class="col-md-3 control-label">Số lượng</label>',
        '<div class="col-md-5">',
        '<div class="qty mt-5">',
        '<span class="minus bg-dark">-</span>',
        '<input type="number" class="count" name="qty" value="1">',
        '<span class="plus bg-dark">+</span>',
        '</div>',
        '</div>',
        '</div>',
        '<hr>',
        '</div>',
    ].join("\n");

    var html = '';
    $.each(items, function (key, value) {
        var data = {
            name: value.text,
        }
        var htmlItem = Mustache.render(template, data);
        html += htmlItem;
    });
    $("#FPTCamera_listCamera").html(html);
    $(".drFPTCamera_KhuyenMai").select2({
        width: "100%"
    });
});

$('#drThietBi_ChonThietBi').on('change', function (e) {
    var items = $(this).select2('data')
    var template = [
        '<div>',
        '<div class="form-group">',
        '<label for="inputEmail3" class="col-md-3 control-label">Thiết bị</label>',
        '<div class="col-md-5">',
        '<input type="text" class="form-control" value="{{name}}" disabled="disabled" id="inputEmail3">',
        '</div>',
        '</div>',
        '<div class="form-group">',
        '<label class="col-md-3 control-label">Số lượng</label>',
        '<div class="col-md-5">',
        '<div class="qty mt-5">',
        '<span class="minus bg-dark">-</span>',
        '<input type="number" class="count" name="qty" value="1">',
        '<span class="plus bg-dark">+</span>',
        '</div>',
        '</div>',
        '</div>',
        '<div class="form-group">',
        '<label class="col-md-3 control-label">CLKM</label>',
        '<div class="col-md-5">',
        '<select class="drThietBi_KhuyenMai form-control select2 select2-hidden-accessible" style="width: 100%;" tabindex="-1" aria-hidden="true">',
        '<option selected="selected">Truyền hình FPT</option>',
        '</select>',
        '</div>',
        '</div>',
        '<hr>',
        '</div>',
    ].join("\n");

    var html = '';
    $.each(items, function (key, value) {
        var data = {
            name: value.text,
        }
        var htmlItem = Mustache.render(template, data);
        html += htmlItem;
    });
    $("#ThietBi_DanhSachChiTiet").html(html);
    $(".drThietBi_KhuyenMai").select2({
        width: "100%"
    });
});

function AddCloud() {
    var template = [
        '<div id="{{id}}" >',
        '<div class="form-group">',
        '<label for="inputEmail3" class="col-md-3 control-label">Tên gói</label>',
        '<div class="col-md-5">',
        '<div class="input-group">',
        '<input type="text" disabled class="form-control" value="{{Tengoi}}" id="inputEmail3" placeholder="[Họ và tên]">',
        '<span class="input-group-btn">',
        '<button class="btn btn-default" type="button" onclick="RemoveCloud({{id}})"><i class="fa fa-trash"></i></button>',
        '</span>',
        '</div>',
        '</div>',
        '</div>',
        '<div class="form-group">',
        '<label class="col-md-3 control-label">Thời hạn</label>',
        '<div class="col-md-5">',
        '<input type="text" class="form-control" disabled="disabled" value="{{Thoihan}}" id="inputEmail3" placeholder="[Họ và tên]">',
        '</div>',
        '</div>',
        '<div class="form-group">',
        '<label class="col-md-3 control-label">Số lượng</label>',
        '<div class="col-md-5" style="display:inline-flex">',
        '<div class="qty mt-5">',
        '<span class="minus bg-dark">-</span>',
        '<input type="number" class="count" name="qty" value="1">',
        '<span class="plus bg-dark">+</span>',
        '</div>',
        '<label style="font-size:15px" class="label label-primary pull-right">{{Gia}} đồng</label>',
        '</div>',
        '</div>',
        '<hr>',
        '</div>',
    ].join("\n");
    var itemSelected = $('#drCloud_Tengoi').select2('data');
    var data = {
        Tengoi: itemSelected[0].text,
        Thoihan: $('#drCloud_Thoihan').val(),
        Soluong: $('#Cloud_SoLuong').val(),
        Gia: 12 * parseInt($('#Cloud_SoLuong').val()),
        id: itemSelected[0].id,
    }
    var html = Mustache.render(template, data);
    $("#FPTCloud_listcloud").append(html);
};

function RemoveCloud(id) {
    $("#" + id).remove();
}

// load danh sách loại hình
function GetCusType() {
    $.ajax({
        url: '/Registration/GetCusType',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: {},
        beforeSend: loading(),
        success: function (result) {
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.ID,
                            text: this.Name
                        });
                    });
                    $("#drCusType").select2({
                        data: data,
                        width: "100%"
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetCustomerType() {
    $.ajax({
        url: '/Registration/GetCustomerType',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: {},
        beforeSend: loading(),
        success: function (result) {
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.ID,
                            text: this.Name
                        });
                    });
                    $("#drCustomerType").select2({
                        data: data,
                        width: "100%"
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetPhoneType() {
    $.ajax({
        url: '/Registration/GetPhoneType',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: {},
        beforeSend: loading(),
        success: function (result) {
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.ID,
                            text: this.Name
                        });
                    });
                    $(".drPhoneType").select2({
                        data: data,
                        width: "100%"
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetHouseType() {
    $.ajax({
        url: '/Location/GetHouseType',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: {},
        beforeSend: loading(),
        success: function (result) {
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.ID,
                            text: this.Name
                        });
                    });
                    $("#drHouseType").select2({
                        data: data,
                        width: "100%"
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetDistrict() {
    $.ajax({
        url: '/Location/GetDistrict',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: {},
        beforeSend: loading(),
        success: function (result) {
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.Name,
                            text: this.FullName
                        });
                    });
                    $("#drDistrict").select2({
                        data: data,
                        width: "100%"
                    });

                    $('#drDistrict').on('select2:select', function (e) {
                        GetWard();
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetWard() {
    var formData = {
        District: $("#drDistrict").val()
    };
    $.ajax({
        url: '/Location/GetWard',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: JSON.stringify(formData),
        beforeSend: loading(),
        success: function (result) {
            $('#drWard').empty().trigger("change");
            $('#drWard').append('<option value="0">Vui lòng chọn phường/xã</option>');
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.Name,
                            text: this.Name
                        });
                    });
                    $("#drWard").select2({
                        data: data,
                        width: "100%"
                    });
                    $('#drWard').on('select2:select', function (e) {
                        GetListStreet();
                        GetNameVilla();
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetListStreet() {
    var formData = {
        District: $("#drDistrict").val(),
        Ward: $("#drWard").val(),
        Type: 0
    };
    $.ajax({
        url: '/Location/GetListStreet',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: JSON.stringify(formData),
        beforeSend: loading(),
        success: function (result) {
            $('#drStreet').empty().trigger("change");
            $('#drStreet').append('<option value="0">Vui lòng chọn đường</option>');
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.option,
                            text: this.option
                        });
                    });

                    $("#drStreet").select2({
                        data: data,
                        width: "100%"
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetNameVilla() {
    var formData = {
        District: $("#drDistrict").val(),
        Ward: $("#drWard").val(),
        Type: 1
    };

    $.ajax({
        url: '/Location/GetListStreet',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: JSON.stringify(formData),
        beforeSend: loading(),
        success: function (result) {
            $('#drNameVilla').empty().trigger("change");
            $('#drNameVilla').append('<option value="0">Vui lòng chọn chung cư</option>');
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.ID,
                            text: this.option
                        });
                    });
                    $("#drNameVilla").select2({
                        data: data,
                        width: "100%"
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}
function GetHouseCoordinate() {
    $.ajax({
        url: '/Location/GetHouseCoordinate',
        method: "POST",
        dataType: "json",
        async: false,
        contentType: "application/json",
        data: {},
        beforeSend: loading(),
        success: function (result) {
            if (result == undefined || result == null || result == "") {
                showalert("Không có dữ liệu", 0);
                return;
            }

            if (result.Code == 1) {
                if (result.Data.length > 0) {
                    var data = [];
                    $(result.Data).each(function () {
                        data.push({
                            id: this.ID,
                            text: this.Name
                        });
                    });
                    $("#drHouseCoordinate").select2({
                        data: data,
                        width: "100%"
                    });
                }
            } else {
                showalert(result.Message, 0);
                return;
            }
        },
        error: function (result, stt, msg) {
            showalert(msg, 0);
            hideloading();
        },
        complete: hideloading()
    });
}