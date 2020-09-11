

var ImageDiaLog = function (option) {
    var vars = {
        MaDMA: '',
        urlGetAlbum: '',
        urlGetFolders: '',
        urlUploadImage: '',
        urlGetImageInfor: '',
        urlRemoveImage: '',
        LoaiAnh: ''
    };
    var listHA;
    this.SetMaDMA = function (id) {
        this.vars.MaDMA = id;
    }
    this.XoaAnh = function (e) {
        var MaAnh = $("#ListView1 .list-item-hover.active").parent().attr('data-name');
        MaAnh = MaAnh.split('.')[0].split('-');
        MaAnh = MaAnh[MaAnh.length - 1];
        $.get(vars.urlRemoveImage + "?MaAnh=" + MaAnh, function (data, status) {
            if (status == "success" && data == "OK") {
                alert("Xóa ảnh thành công");
                var id = $('#jstree_div').jstree("get_selected", true)[0].id;
                $('#jstree_div').jstree("deselect_all", true);
                $("#jstree_div").jstree('select_node', id);
            } else {
                alert("Xảy ra lỗi");
            }
        });
    }
    this.construct = function (option) {
        $.extend(vars, option);
        listHA = new DSHinhAnh();

        $(window).resize(function () {

            InitalSize();
        });
        $('#jstree_div').jstree({
            core: {
                check_callback: true
            }.
        });

        $(".file-manager").click(function () {
            $("#UploadFile").addClass("d-none");
            $("#UploadFile").removeClass("d-block");
            $("#FileManager").removeClass("d-none");

            var id = $('#jstree_div').jstree("get_selected", true)[0].id;
            $('#jstree_div').jstree("deselect_all", true);
            $("#jstree_div").jstree('select_node', id);
        });
        $(".upload").click(function () {
            $("#UploadFile").addClass("d-block");
            $("#FileManager").addClass("d-none");
            $("#UploadFile").removeClass("d-none");
            $(".server-path").text("Đường dẫn server: " + window.location.protocol + "//" + window.location.host + $('#jstree_div').jstree("get_selected", true)[0].data);
        });
        $(".nav-link.item-file.bg-close").on("click", function () {
            $(".imagedialog").css("display", "none");
            document.oncontextmenu = function () { return true; };
        });
    };
    var InitalSize = function () {
            $('.imagedialog .slimScrollDiv').children().unwrap();
            $('.imagedialog .slimScrollBar, .imagedialog .slimScrollRail').remove();
        if ($(window).width() < 1200) {
            if ($(window).width() < 800) {
                $(".imagedialog").css("left", "10px");
                $(".imagedialog").css("right", "10px");
                $(".imagedialog").css("top", "10%");
                $(".imagedialog").css("bottom", "10%");
            } else {
                $(".imagedialog").css("left", "5%");
                $(".imagedialog").css("right", "5%");
                $(".imagedialog").css("top", "5%");
                $(".imagedialog").css("bottom", "5%");
            }
        } else {
            $(".imagedialog").css("left", "15%");
            $(".imagedialog").css("right", "15%");
            $(".imagedialog").css("top", "15%");
            $(".imagedialog").css("bottom", "15%");
        }
        var height = $(".imagedialog").height();
        var widht = $(".imagedialog").width();
        itemHeight = height - $("#menu-image").height() - 2;
        $("#jstree_div").css("height", itemHeight + "px");
        $('#ListView1').slimScroll({
            height: itemHeight + "px",
            color: 'rgba(0,0,0,0.5)',
            size: '4px',
            position: 'right',
            alwaysVisible: true,
            railBorderRadius: '1px',
            borderRadius: '1px',
        });
        $("#jstree_div").slimScroll({
            height: itemHeight + "px",
            color: 'rgba(0,0,0,0.5)',
            size: '3px',
            position: 'right',
            alwaysVisible: false,
            railBorderRadius: '1px',
            borderRadius: '1px',
        });
        itemHeight = $(".imagedialog").height() - $("#menu-image").height() - $(".header").innerHeight() - 20;

        $("#accordion").css("height", itemHeight + "px");
        $("#accordion").slimScroll({
            height: itemHeight + "px",
            color: 'rgba(0,0,0,0.5)',
            size: '3px',
            position: 'right',
            alwaysVisible: false,
            railBorderRadius: '1px',
            borderRadius: '1px',
        });

    };
    var UnselectedAll = function () {
        $("#ListView1 .list-item").each(function () {

            var childitem = $(this).children(".list-item-hover");
            if (childitem.hasClass("active")) {
                childitem.removeClass("active");
            }
        });
    };
    var ProccessSelected = function (listitem) {
        if (listitem.hasClass("list-item-hover") && listitem.parent().attr("data-type") == 'Folder') {
            $('#jstree_div').jstree("deselect_all", true);
            $("#jstree_div").jstree('select_node', listitem.parent().attr("id"));
        }
        if (listitem.hasClass("list-item-hover") && listitem.parent().attr("data-type") == 'Photo') {
            var id = $(".imagedialog").attr("data-id");
            if (id == 1) {
                var str = listitem.parent().attr("data-name");
                var tmp = str.split(".");
                tmp = tmp[0].split("-");

                $.get(vars.urlGetImageInfor + "?id=" + tmp[tmp.length - 1], function (data, status) {
                    $(".cke_dialog_ui_hbox_first:first").find("input[type='text']").val(window.location.protocol + "//" + window.location.host + data.Path + "/" + str);
                    $(".cke_dialog_ui_hbox_first:first").find("input[type='text']").change();
                    $(".cke_dialog_ui_hbox_first:first").parent().parent().find(".cke_dialog_ui_vbox_child:fist").find("input[type='text']").val(data.Tag);
                    $(".imagedialog").css("display", "none");

                    $(".imagedialog").trigger("selected", [window.location.protocol + "//" + window.location.host + data.Path + "/" + str, data.Tag]);
                    $(".imagedialog").off();
                    document.oncontextmenu = function () { return true; };
                    //alert($(".cke_dialog_ui_vbox_child").length);
                    return;
                });
            }
        }
        
    };
    this.CreateListView = function () {
        $(document).on("mousedown", "#ListView1", function (e) {
           
            if (e.button == 2) {
                var menu = $("#mrclick");
                var listitem = $(e.target);
                var dx = 0, dy = 0;
                if (listitem.hasClass("list-item-hover")) {
                    dx = $(e.target).offset().left - $("#ListView1").offset().left
                    dy = $(e.target).offset().top - $("#ListView1").offset().top 
                    menu.children("a:contains('Xóa ảnh')").css('display', 'block');
                   // console.log(dx + ":" + dy);
                    UnselectedAll();
                    listitem.addClass("active");
                } else {
                    menu.children("a:contains('Xóa ảnh')").css('display', 'none');
                }
                $("#mrclick > a").attr("data-id", 0);
                menu.css("top", dy + e.offsetY + "px");
                menu.css("left", dx +  e.offsetX + "px");
               
                if (menu.css('display') == "none") {
                    menu.slideToggle(300);

                }
               
            }
        });
        $(document).on("click", function (e) {
            var listitem = $(e.target);
            if (listitem.hasClass("list-item-hover") && listitem.hasClass("active")) {

                ProccessSelected(listitem);
            }
            if (listitem.hasClass("list-item-hover")) {
                UnselectedAll();
                listitem.addClass("active");
            }
          
            if (listitem.not($("#mrclick"))) {
                $("#mrclick").slideUp();
                return;
            } 
        });
        $("#ListView1").dblclick(function (e) {
            var listitem = $(e.target);
            ProccessSelected(listitem);
           
        });

    };
    this.InitFileUpload = function () {

        $("button[type='button'] > span:contains('Thêm Ảnh')").on("click", function () {
            $("input[type='file']").trigger("click");
        });
        $("#accordion").on("click", "button", function (e) {
            var id = $(this).parents("div.card").attr('data-id');
            if ($(e.target).text() == "Xóa Ảnh") {
                listHA.XoaAnh(id);
                $(this).parents("div.card").remove();
            }
            if ($(e.target).text() == "Hủy") {
                var t = listHA.LayAnh(id);
                var p = $(this).parents(".card-body");
                p.find("#tag").val(t.obj.Tag);
                p.find("#text").val(t.obj.TenAnh);
                p.find("#sel1").val(t.obj.MaDM);
            }
            if ($(e.target).text() == "Lưu thông tin") {
                var t = listHA.LayAnh(id);
                var p = $(this).parents(".card-body");

                t.obj.Tag = p.find("#tag").val();
                t.obj.MaDM = p.find("#sel1").val();
                t.obj.TenAnh = p.find("#text").val();
                $("#card-title").html(t.obj.TenAnh + "." + t.obj.FS);
                listHA.SuaAnh(t);
            }

        });
        document.getElementById('file').addEventListener('change', handleFileSelect, false);
        $("#FormUpload").submit(function (e) {
            e.preventDefault();
            $('#accordion .collapse').collapse('hide');
            $(".progress").css("display", "block");
            var path = $('#jstree_div').jstree("get_selected", true)[0].data;

            for (var i = 0; i < listHA.Count(); i++) {
                var xhr = new XMLHttpRequest();
                xhr.open('POST', vars.urlUploadImage, true);
                var item = listHA.list[i].obj;
                xhr.setRequestHeader("Content-Type", "multipart/form-data");
                xhr.setRequestHeader("Path", path);
                xhr.setRequestHeader("File-type", item.FS);
                xhr.setRequestHeader("TenAnh", item.TenAnh);
                xhr.setRequestHeader("MaDMA", vars.MaDMA);
                xhr.setRequestHeader("LoaiAnh", item.MaDM);
                xhr.setRequestHeader("Tag", item.Tag);

                (function (id, stt) {
                    xhr.addEventListener("progress", function (e) {
                        f(e, id, stt);
                    }, false);
                })(i, item.STT);

                // console.log(listHA.list);
                //  console.log(i);
                //console.log(item.image);
                xhr.send(item.image);


            }
            $("#FormUpload")[0].reset();
        });
    };
    var f = function (e, id, stt) {

        if (e.lengthComputable) {
            //console.log(id);
            var progressBar = $(".card[data-id='" + stt + "']").find(".progress-bar");
            var percentComplete = e.loaded / e.total;
            percentComplete = parseInt(percentComplete * 100);
            //   console.log(percentComplete);
            progressBar.css("width", percentComplete + "%");
            progressBar.html(percentComplete + "%");


            if (percentComplete == 100) {
                listHA.XoaAnh(stt);
                setTimeout(function () {
                    var p = progressBar.parents(".card-header")
                    p.addClass("bg-success");
                    p.find(".card-link[data-name='link-expand']").css("display", "none");
                    var c = p.find(".card-link[data-name='link-close']");
                    c.css("display", "block");
                    c.click(function () {
                        $(this).parents("div.card").remove();
                    });
                }, 1000);

            }

        }
    }
    function Valid() {
        this.val();
        this.val("ASdasd");
    }
    var handleFileSelect = function (evt) {
        // alert("fgfgh");
        var files = $('#file').get(0).files;

        for (var i = 0; i < files.length; i++) {
            var f = files[i];

            var reader = new FileReader();
            var img = new HinhAnh({});
            img.obj.image = f;
            img.obj.STT = listHA.Count() + 1;
            img.obj.MaDM = 1;
            listHA.list.push(img);
            var tmp = listHA.Count() - 1;
            // Closure to capture the file information.
            reader.onload = ImageOnLoad;
            reader.readAsDataURL(f);
        }
        $('#file').val("");
        // console.log(listHA.list);
    };
    var ImageOnLoad = function (theFile) {
        var tmp1 = tmp;
        var str = theFile.name.split('.');
        listHA.list[tmp].obj.FS = str[str.length - 1]
        str.splice(str.length - 1, 1);


        listHA.list[tmp].obj.TenAnh = str.join("");
        var url = window.URL || window.webkitURL;
        var image = new Image();

        image.onload = function () {
            var fSize = f.size;
            var fSExt = new Array('Bytes', 'KB', 'MB', 'GB');
            var j = 0;
            while (fSize > 900) { fSize /= 1024; j++; }
            var img1 = listHA.list[tmp1];

            img1.obj.Path = image.src;
            img1.obj.Tag = "";

            img1.obj.size = (Math.round(fSize * 100) / 100) + " " + fSExt[j];
            img1.obj.width = image.width;
            img1.obj.height = image.height;

            $('#accordion .collapse').collapse('hide');
            $("#filetemplate").tmpl({ "HinhAnh": img1.obj, "Proccess": false, "Percentage": "0", "LoaiAnh": vars.LoaiAnh }).appendTo("#accordion");
        };
        image.onerror = function () {
            // alert('Invalid image');
        };

        image.src = url.createObjectURL(f);
    }
    this.CreateTreeView = function () {

        $.ajax({
            url: vars.urlGetAlbum + "?MaDMA=" + vars.MaDMA,
            type: "GET",
            dataTypes: "JSON",
            success: function (data) {
                var obj = $.parseJSON(JSON.stringify(data));

                //alert(obj.ParentFolder.Path);
                $('#jstree_div').jstree().create_node(null, { "id": obj.ParentFolder.FolderName, "text": obj.ParentFolder.FolderName, "state": { "opened": true }, "data": obj.ParentFolder.Path }, "last");
                // alert(obj.ImageGallerys);
                //  console.log(obj);
                if (!$.isEmptyObject(obj.ImageGallerys)) {
                    CreateNode(obj.ParentFolder.FolderName, obj.ImageGallerys);

                }
                $("#jstree_div").jstree('select_node', obj.ParentFolder.FolderName);

            }

        });

        $("#jstree_div").on('changed.jstree', function (e, data) {
            //  alert(data.node.data);
            //  var obj = GetAllFolder(data.node.id, data.node.data);
            GetAllFolder(data.node.id, data.node.data, function (obj) {
                $('#ListView1').children().html("");
                if (!$.isEmptyObject(obj.ImageGallerys)) {
                    for (var i = 0; i < obj.ImageGallerys.length; i++) {

                        $('#ListView1').children().append(AddListViewItem(obj.ImageGallerys[i].FolderName, 'Folder', obj.ImageGallerys[i].FolderName, obj.ImageGallerys[i].PathImageFolder));
                    }
                }
                if (!$.isEmptyObject(obj.Images)) {
                    for (var i = 0; i < obj.Images.length; i++) {

                        var path = obj.Images[i].Path;

                        $('#ListView1').children().append(AddListViewItem(obj.Images[i].FileName, 'Photo', obj.Images[i].FileName, path));
                    }
                }
            });

        });
        $("#jstree_div").on('open_node.jstree', function (e, data) {
            var node = $("#jstree_div").jstree(true).get_node(data.node.children[0]);
            var currChildNode = "tmp" + data.node.id;
            if (node.id == currChildNode) {
                GetAllFolder(data.node.id, data.node.data, function (obj) {
                    $("#jstree_div").jstree(true).delete_node($("#jstree_div").jstree(true).get_node(currChildNode));

                    if (!$.isEmptyObject(obj.ImageGallerys)) {
                        CreateNode(data.node.id, obj.ImageGallerys);
                    }
                });
            }

        });

    };//
    var GetAllFolder = function (name, path, handle) {

        $.ajax({
            url: vars.urlGetFolders + "?name=" + name + "&path=" + path,
            type: "GET",
            dataTypes: "JSON",
            success: function (data) {
                handle(JSON.parse(JSON.stringify(data)));
            }
        });
    };
    this.Show = function (id) {
        document.oncontextmenu = function () { return false; };
        $(".imagedialog").css("display", "block");
        $(".imagedialog").attr("data-id", id);
        InitalSize();
    };
    var CreateNode = function (parent, obj) {
        //alert(obj.length);
        for (i = 0; i < obj.length; i++) {
            $('#jstree_div').jstree().create_node(parent, { "id": obj[i].FolderName, "text": obj[i].FolderName, "data": obj[i].Path }, "last");
            $('#jstree_div').jstree().create_node(obj[i].FolderName, { "id": "tmp" + obj[i].FolderName, "text": "" }, "last");
            if (!$.isEmptyObject(obj.ImageGallerys)) {
                CreateNode(obj[i].NameGalery, obj.ImageGallerys);
            }
        }
    };
           
    var AddListViewItem = function (id, type, name, path) {
        return '<div class="list-item" id="' + id + '" data-type="' + type + '" data-name="' + name + '"><div class="list-item-hover"></div>' +
            '<div class="d-flex flex-column">' +
            '<div class="contain-image">' +
            '<img src="' + path + '" />' +
            '</div>' +
            '<span class="text-center pb-2">' + name + '</span>' +
            '</div>' +
            '</div>';
    };
};

var HinhAnh = function (option) {

    this.obj = {
        STT: '',
        FS: '',
        MaDM: '',
        TenAnh: '',
        Path: '',
        Tag: '',
        size: '',
        height: '',
        width: '',
        image: undefined
    };
    this.construct = function (option) {
        $.extend(this.obj, option);

    };
    this.construct(option);


}
var DSHinhAnh = function () {
    this.list = new Array();
    this.construct = function () {

    };
    this.ThemAnh = function (obj) {

        this.list.push(obj);

    };

    this.Count = function () {
        return this.list.length;
    };
    this.SuaAnh = function (stt, obj) {
        var index = this.list.findIndex(x => x.obj.STT == stt);
        this.list[index] = obj;
    }
    this.LayAnh = function (stt) {
        return this.list.find(x => x.obj.STT == stt);
    };
    this.XoaAnh = function (stt) {
        var index = this.list.findIndex(x => x.obj.STT == stt);
        this.list.splice(index, 1);

    }
    this.construct();
};


