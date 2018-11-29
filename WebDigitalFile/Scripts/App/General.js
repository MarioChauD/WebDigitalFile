
var verificarCierre;
var PDR_anhio;

$(document).ready(function () {

    /*Esta línea de cóigo permite que en el caso la página se bloquee por una consulta Ajax
      esta se desbloquee cuando esta haya terminado
    */
    //$(document).ajaxStop($.unblockUI);


    /*>>>>>>>>>>Abre ventana de confirmación<<<<<<<<<<*/
    window.abrirVentanaModalConfirmacion = function (mensaje, funcionConfirmacion) {
        $("#mensaje-dialogo").text(mensaje);

        $("#modal-window").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            title: "Ventana de Confirmación",
            buttons: {
                "Si": function () {
                    $(this).dialog("close");
                    funcionConfirmacion();
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    };

    /*>>>>>>>>>>Abre ventana de dialogo<<<<<<<<<<*/
    window.abrirVentanaModalDialogo = function (mensaje) {
        $("#mensaje-dialogo").text(mensaje);

        $("#modal-window").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            title: "Mensaje PDR",
            buttons: {
                Ok: function () {
                    $(this).dialog("close");
                }
            }
        });
    };

    var ruta = "http://" + PRD_ROOT;
    var cierraOK = function (mostrarMensaje) {
        var rpta = true;
        if (typeof (verificarCierre) != "undefined") {
            rpta = verificarCierre();
            if (!rpta && mostrarMensaje) {
                rpta = confirm('Datos sin grabar\n Seguro que quiere salir?');
            }
        }
        return rpta;
    };

    /*>>>>>>>>>>Obtiene el script Html del checkbox que indica si se mostrará o no a los empleados activos<<<<<<<<<<*/
    var obtenerHtmlCheckMostrarSoloClientes = function () {
        return html = "<label>   Mostrar solo Empleados Activos: <input type='checkbox' id='chk_estado_personal' style='width:15px;' checked=true /></label>";
    };

    var initTB = function () {
        var itemsTB;

        itemsTB = [
                {
                    type: "button",
                    //imageUrl: "kendo/content/web/treeview/file.png",
                    imageUrl: (ruta + "/kendo/content/web/16x16/ComboBox.png"),
                    text: "Evaluación",
                    click: clickEvaluacion,
                },
                {
                    type: "splitButton",
                    imageUrl: ruta + "/Content/images/utiles/periodo_16x16.png",
                    text: "Período PDR",
                    menuButtons: cargarPeriodosPDR(),
                    click: clickPeriodoPDR,
                },
                {
                    template: obtenerHtmlCheckMostrarSoloClientes()
                },
                {
                    type: "separator"
                },
                {
                    type: "button",
                    //imageUrl: "kendo/content/web/delete.png",
                    imageUrl: ruta + "/kendo/content/web/16x16/Treeview.png",
                    text: "Supervisores",
                    click: clickSupervisores,
                },
                {
                    type: "button",
                    //imageUrl: "kendo/content/web/edit.png",
                    imageUrl: ruta + "/kendo/content/web/16x16/usuarios16x16.png",
                    text: "Usuarios",
                    click: clickUsuarios,
                },
                {
                    type: "separator"
                },
                {
                    type: "splitButton",
                    text: "Reportes PDR",
                    imageUrl: ruta + "/Content/images/utiles/report_16x16.png",
                    menuButtons: cargarListaReportesPDR(),
                    click: clickReportes,
                },
                {
                    type: "splitButton",
                    text: "Mantenimientos",
                    imageUrl: ruta + "/Content/images/utiles/maintenance_16x16.png",
                    menuButtons: [{ text: "Mantenimiento de Áreas Agrupadas", id: "MantenimientoAreasAgrupadas" },
                    { text: "Calibración de Resultado de Evaluaciones", id: "MantenimientoCalibracionEvaluaciones" }],
                    click: clickMantenimientos,
                },
                {
                    type: "button",
                    imageUrl: ruta + "/kendo/content/web/16x16/1414617560_exit.png",
                    text: "Salir",
                    click: clickSalir,
                }
        ];

        if (PDR_Admin != "1")
            //****siguiente linea por eliminar****
            //itemsTB.splice(1, 2);
            itemsTB.splice(2, 7);

        //muestra el panel con el label del período en consulta de PDR
        $("#panel_anio_consulta").show();

        $("#tabpage1_toolbar").kendoToolBar({
            items: itemsTB
        });
    };

    /*>>>>>>>>>>Carga los periodos de los PDRs<<<<<<<<<<*/
    var cargarPeriodosPDR = function () {
        var listaPeriodosPDR = [];

        $.ajax({
            url: "http://" + PRD_ROOT + "/Api/Evaluacion/ObtenerPeriodosPDR",
            type: 'GET',
            async: false,
            dataType: 'json',
            error: function (xhr, status) {
                alert("Sucedió un error al intentar cargar los períodos PDR");
            },
            success: function (result) {
                for (i = 0; i < result.length; i++) {
                    listaPeriodosPDR.push({ text: result[i].DESCRIP_V_CODE, id: result[i].DESCRIP_V_CODE });
                }
            }
        });

        return listaPeriodosPDR;
    };

    /*>>>>>>>>>>Carga la lista de reportes PDR disponibles<<<<<<<<<<*/
    var cargarListaReportesPDR = function () {
        var listaReportesPDR = [];

        $.ajax({
            url: "http://" + PRD_ROOT + "/Api/Reportes/ObtenerListaReportes",
            type: "GET",
            async: false,
            dataType: "json",
            error: function (xhr, status) {
                abrirVentanaModalDialogo("Se produjo una excepción al intentar obtener la lista de reportes PDR. " + xhr + ". " + status);
            },
            success: function (result) {
                if (result.RPTA == -1) {
                    abrirVentanaModalDialogo(result.MENSAJE);
                } else if (result.RPTA == 1) {
                    for (i = 0; i < result.LISTA_REPORTES.length; i++) {
                        listaReportesPDR.push({ text: result.LISTA_REPORTES[i].DESCRIP_V_DESCRIP, id: result.LISTA_REPORTES[i].DESCRIP_V_CODE });
                    }
                }
            }
        });

        return listaReportesPDR;
    };

    var showLogin = function (mensaje, runOK) {
        var template = kendo.template($("#login-template").html());
        var loginHtml = template;
        $("#login-window").html(loginHtml);
        $("#primaryTextButton").kendoButton({
            click: function (e) {

                var camposValidar = [{ campo: "#login_usuario", mensaje: "Usuario" },
                                     { campo: "#login_password", mensaje: "Contraseña" },
                ];
                for (var i = 0; i < camposValidar.length; i++) {
                    if (($(camposValidar[i].campo).val()).trim() == "") {
                        //Mensaje para login_mensaje_2
                        $("#login_mensaje_2").text("Ingrese " + camposValidar[i].mensaje);
                        $("#login_mensaje_2").show();
                        $(camposValidar[i].campo).focus();
                        return false;
                    }
                }
                //Autentificación
                //****siguiente linea por eliminar****
                //var model = { Usuario: ($("#login_usuario").val()).trim(), Password: ($("#login_password").val()).trim(), Anhio: ($("#login_anhio").val()).trim() };
                var model = { Usuario: ($("#login_usuario").val()).trim(), Password: ($("#login_password").val()).trim(), Anhio: "" };

                //****siguiente linea por eliminar****
                //PDR_anhio = ($("#login_anhio").val()).trim();
                $.ajax({
                    url: "http://" + PRD_ROOT + "/Api/Seguridad/SignIn",
                    type: 'POST',
                    data: JSON.stringify(model),
                    contentType: "application/json;charset=utf-8",
                    success: function (result) {
                        if (result.RPTA == 1) {
                            //alert("Autentificación exitosa");
                        }
                        else {
                            $("#login_mensaje_1").text(result.MENSAJE);
                            $("#login_mensaje_1").show();
                            return;
                        }

                        PDR_anhio = result.PDRanhio;
                        $("#login_mensaje_2").text("");
                        $("#login_mensaje_2").hide();
                        $("#login-window").data("kendoWindow").close();
                        /*
                        if (typeof (runOK) != "undefined")
                            runOK();
                            */

                        window.location.href = "http://" + PRD_ROOT;
                    },
                    error: function () {
                        alert("Sucedió un error al comunicarse con el servidor Web.")
                    },
                });
            }
        });
        if (typeof (mensaje) != "undefined" && mensaje.trim() != "")
            $("#login_mensaje_1").text(mensaje);
        else
            $("#login_mensaje_1").hide();
        $("#login_mensaje_2").hide();
        $("#login-window").kendoWindow({
            //width: "500px",
            title: "PDR - Identificación",
            actions: ["Close"],
            resizable: false,
            modal: true,
        });
        $("#login-window").parent().find(".k-window-action").css("visibility", "hidden");
        $("#login-window").data("kendoWindow").center();
        if (mensaje.trim() != "") {
            //Mensaje para login_mensaje_1

        }
        $("#login_usuario").focus();
        ///
        $('#login_password').keypress(function (event) {
            if (event.keyCode == 13) {
                $('#primaryTextButton').click();
            }
        });

        $("#panel_anio_consulta").hide();

        /*COMENTADO POR REQUERIMIENTO 01-2018 (MEJORAS PDR)*/
        // var dataAnhio = [
        //                { text: "2017", value: "2017" },
        //                { text: "2018", value: "2018" },
        //            ];
        //$("#login_anhio").kendoDropDownList({
        //    dataTextField: "text",
        //    dataValueField: "value",
        //    dataSource: dataAnhio,
        //    index: 1
        //});

        ///

    };
    var clickEvaluacion = function (e) {
        /*if (!cierraOK(true))
            return;
        */

        window.location.href = "http://" + PRD_ROOT + "/Opcion/Evaluacion/";
    }

    var clickSupervisores = function (e) {
        /*if (!cierraOK(true))
            return;
        */
        window.location.href = "http://" + PRD_ROOT + "/Opcion/Supervisores";
    }

    var clickUsuarios = function (e) {
        /*if (!cierraOK(true))
            return;
        */
        window.location.href = "http://" + PRD_ROOT + "/Opcion/Usuarios";
    }

    var clickReportes = function (e) {
        if (typeof (e.id) == "undefined") {
            return;
        }

        accionReporte = e.id;
        window.location.href = "http://" + PRD_ROOT + "/ReportePDR/" + accionReporte;
    }

   
    /*>>>>>>>>>>  <<<<<<<<<<*/
    var clickSalir = function (e) {
        if (!cierraOK(true))
            return;
        verificarCierre = undefined;

        $.ajax({
            url: "http://" + PRD_ROOT + "/Api/Seguridad/SignOut",
            type: 'GET',
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                window.location.href = "http://" + PRD_ROOT;
            },
            error: function () {
                alert("Sucedió un error al comunicarse con el servidor Web.")
            },
        });
    }

    if (PDR_Usuario == "")
        showLogin("", initTB);
    else {
        initTB();
    }
    $(window).bind('beforeunload', function () {
        if (!cierraOK(false)) {
            return 'Datos sin grabar\n Seguro que quiere salir?';
        }
    });

});
