/// <reference path="../global-const.js" />
$(document).ready(function () {
    ! function (e) {
        "use strict";
        var AppConfig = RootURL.prefixURL;
        e.FileDialog = function (a) {
            var o = e.extend(e.FileDialog.defaults, a),
                t = e([

                    "<div class='modal fade'>",
                    "    <div class='modal-dialog modal-md modal-dialog-top'>",
                    "        <div class='modal-content'>",
                    "           <form action='/Uploads/UploadFiles' method='post' enctype = 'multipart/form-data' >",
                    "            <div class='modal-header bg-primary'>",
                    "                <h5 class='modal-title text-white' id='attachEmail'>Add Attachment</h5><button aria-label='Close' class='close' data-dismiss='modal' type='button'><span aria-hidden='true'>&times;</span></button>",
                    "            </div>",
                    "            <div class='modal-body'>",
                    "             <h6 class='text-dark cg-bold mb-3' id='name-header'>[Name]</h6>",
                    "                <div class='bfd-dropfield'>",
                    "                  <div class='bfd-dropfield-inner cg-bold'>", o.drag_message,
                    "                    <div class='bfd-dropfield-mid'>", o.or,
                    " 				     </div>",
                    "                    <label class='btn btn-outline-primary px-5'>",
                    "                       Browse Files <input type='file' name='files' id='uploadFile' multiple style='display: none;'/>",
                    "                    </label>",
                    //"                    <input type='file' name='mainfiles' style='display:none;' />",
                    "                  </div>",
                    "                </div>",
                    "                <div class='container-fluid bfd-files'>",
                    "                </div>",
                    "            </div>",
                    "           <input type='hidden' name='recordId' id='recordId'/>",
                    "           <input type='hidden' name='removeFiles' id='removeFiles'/>",
                    "           <input type='hidden' name='pointerUpload' id='pointerUpload'/>",
                    "			<div class='modal-footer'>",
                    "         		<div class='w-100'>",
                    "           		<button class='btn btn-outline-primary'",
                    "									data-dismiss='modal'>", o.cancel_button,
                    "					</button>",
                    "           		<button type='submit' onclick='return SubmitUploadFiles()' class='btn btn-primary float-right bfd-cancel'>", o.ok_button,
                    "					</button>",
                    "         		</div>",
                    "       	</div>",
                    "           </form>",
                    "        </div>",
                    "    </div>",
                    "</div>"

                ].join("")),
                n = !1,
                r = e("input:file", t),
                i = e(".bfd-dropfield", t),
                s = e(".bfd-dropfield-inner", i);
            s.css({
                height: o.dropheight,
                "padding-top": o.dropheight / 2 - 58
            }), r.attr({
                accept: o.accept,
                multiple: o.multiple
            }), i.on("click.bfd", function () {
                r.trigger("click")
            });
            var d = [],
                l = [],
                c = function (a) {
                    var n, r, i = new FileReader;
                    l.push(i), i.onloadstart = function () { }, i.onerror = function (e) {
                        e.target.error.code !== e.target.error.ABORT_ERR && n.parent().html(["<div class='bg-orange bfd-error-message'>", o.error_message, "</div>"].join("\n"))
                    }, i.onprogress = function (a) {
                        var o = Math.round(100 * a.loaded / a.total) + "%";
                        n.attr("aria-valuenow", a.loaded), n.css("width", o), e(".sr-only", n).text(o)
                    }, i.onload = function (e) {
                        a.content = e.target.result, d.push(a), n.removeClass("active")
                    };
                    var s = e(["<div class='col-12 pl-0 py-1 cg-bold bfd-info'>",
                        "    " + a.name, "</div>",
                        "<div class='col-11 px-0 bfd-progress'>",
                        "    <div class='progress'>",
                        "        <div class='progress-bar bg-secondary active' role='progressbar'",
                        "            aria-valuenow='0' aria-valuemin='0' aria-valuemax='" + a.size + "'>",
                        "            <span class='sr-only'>0%</span>",
                        "        </div>",
                        "    </div>",
                        "</div>",
                        "	   <div class='col-1'>",
                        "    <a class='remove-item' onclick='RemoveAttachment(this)' href='#'>X</a>&nbsp;",
                        "    </div>"

                    ].join(""));
                    n = e(".progress-bar", s), e(".bfd-remove", s).tooltip({
                        container: "body",
                        html: !0,
                        placement: "top",
                        title: o.remove_message
                    }).on("click.bfd", function () {
                        var e = d.indexOf(a);
                        e >= 0 && d.pop(e), r.fadeOut();
                        try {
                            i.abort()
                        } catch (o) { }
                    }), r = e("<div class='row'></div>"), r.append(s), e(".bfd-files", t).append(r), i["readAs" + o.readAs](a)
                },
                f = function (e) {
                    Array.prototype.forEach.apply(e, [c])
                };
            return r.change(function (e) {
                e = e.originalEvent;
                var a = e.target.files;
                f(a);
                var o = r.clone(!0);
                r.replaceWith(o), r = o
            }), i.on("dragenter.bfd", function () {
                s.addClass("bfd-dragover")
            }).on("dragover.bfd", function (e) {
                e = e.originalEvent, e.stopPropagation(), e.preventDefault(), e.dataTransfer.dropEffect = "copy"
            }).on("dragleave.bfd drop.bfd", function () {
                s.removeClass("bfd-dragover")
            }).on("drop.bfd", function (e) {
                e = e.originalEvent, e.stopPropagation(), e.preventDefault();
                var a = e.dataTransfer.files;
                0 === a.length, f(a)
            }), e(".bfd-ok", t).on("click.bfd", function () {
                var a = e.Event("files.bs.filedialog");
                a.files = d, t.trigger(a), n = !0, t.modal("hide")
            }), t.on("hidden.bs.modal", function () {
                if (l.forEach(function (e) {
                    try {
                        e.abort()
                    } catch (a) { }
                }), !n) {
                    var a = e.Event("cancel.bs.filedialog");
                    t.trigger(a)
                }
                t.remove()
            }), e(document.body).append(t), t.modal(), t
        }, e.FileDialog.defaults = {
            accept: "*",
            cancel_button: "Cancel",
            drag_message: "Drop & Drop Files Here",
            or: "or",
            dropheight: 150,
            error_message: "An error occured while loading file",
            multiple: !0,
            ok_button: "Save",
            readAs: "DataURL",
            remove_message: "Remove&nbsp;file",
            title: "Attach Document",
            url: AppConfig
        }
    }(jQuery);

    OpenAttachment = function (pointerUpload,Id, ClientName) {
        $.FileDialog({ multiple: true }).on('files.bs.filedialog', function (ev) {
            var files = ev.files;
            var text = "";
            files.forEach(function (f) {
                text += f.name + "<br/>";
            });
            $("#output").html(text);
        }).on('cancel.bs.filedialog', function (ev) {
            $("#output").html("Cancelled!");
        });
        $("#name-header").text(ClientName);
        $("#recordId").val(Id);
        $("#pointerUpload").val(pointerUpload);
    };

    RemoveAttachment = function (event) {
        var currentItem = $(event).parent('div').parent('div').find('.cg-bold.bfd-info').text().trim();
        lengthOfFiles = document.getElementsByName("files")[0].files.length;
        var newFileList = [];
        var currentRemoveFiles = $("#removeFiles").val();
        if (currentRemoveFiles !== "") newFileList.push(currentRemoveFiles);
        
        if (lengthOfFiles > 0) {
            var removeFile = "";
            for (var i = 0; i < lengthOfFiles; i++) {
                removeFile = document.getElementsByName("files")[0].files[i].name.trim();
                if (currentItem === removeFile) {
                    newFileList.push(removeFile);
                }
            }
        }
        document.getElementById("removeFiles").value = newFileList;
        
        $(event).parent('div').parent('div').remove();
    };

    SubmitUploadFiles = function () {
        var allFiles = '';
        allFiles = document.getElementsByName("files").value;
        return true;
    };
});


