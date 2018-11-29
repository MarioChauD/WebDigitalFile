
function soloFecha(fecha) {
    if (fecha != undefined && fecha.value != "") {
        if (!/^\d{2}\/\d{2}\/\d{4}$/.test(fecha.value)) {
            alert("formato de fecha inválida (dd/mm/aaaa)");
            return false;
        }
        var dia = parseInt(fecha.value.substring(0, 2), 10);
        var mes = parseInt(fecha.value.substring(3, 5), 10);
        var anio = parseInt(fecha.value.substring(6), 10);

        switch (mes) {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                numDias = 31;
                break;
            case 4: case 6: case 9: case 11:
                numDias = 30;
                break;
            case 2:
                if (comprobarSiBisisesto(anio)) { numDias = 29 } else { numDias = 28 };
                break;
            default:
                alert("Fecha introducida erronea");
                return false;
        }

        if (dia > numDias || dia == 0) {
            alert("Fecha introducida erronea");
            return false;
        }
        return true;
    }
}

function comprobarSiBisisesto(anio) {
    if ((anio % 100 != 0) && ((anio % 4 == 0) || (anio % 400 == 0))) {
        return true;
    }
    else {
        return false;
    }
}



function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}
function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}


function _CloseOnEsc() {
    if (event.keyCode == 27) {
        window.close();
        return;
    }
}

function isDate(dateStr) {
    //Modified by DO 20/12/2010
    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArray = dateStr.match(datePat); // is the format ok?

    if (matchArray == null) {
        //alert("Please enter date as either mm/dd/yyyy or mm-dd-yyyy.");
        return false;
    }

    // p@rse date into variables
    day = matchArray[1];
    month = matchArray[3];
    year = matchArray[5];

    if (month < 1 || month > 12) { // check month range
        //alert("Month must be between 1 and 12.");
        return false;
    }

    if (day < 1 || day > 31) {
        //alert("Day must be between 1 and 31.");
        return false;
    }

    if ((month==4 || month==6 || month==9 || month==11) && day==31) {
        //alert("Month "+month+" doesn`t have 31 days!")
        return false;
    }

    // check for february 29th
    if (month == 2) { 
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29 || (day==29 && !isleap)) {
            //alert("February " + year + " doesn`t have " + day + " days!");
            return false;
        }
    }
    
    // date is valid
    return true;
}

function darMayuscula(obj)
{
    var str=trim(obj.value);
    obj.value=str.toUpperCase();
}

// moneyFormat
function moneyFormat(amount) { 
    var val = parseFloat(amount); 
    if (isNaN(val)) { return "0.00"; } 
    if (val <= 0) { return "0.00"; } 
    val += ""; 
    // Next two lines remove anything beyond 2 decimal places 
    if (val.indexOf('.') == -1) { return val+".00"; } 
    else { val = val.substring(0,val.indexOf('.')+3); } 
    val = (val == Math.floor(val)) ? val + '.00' : ((val*10 == Math.floor(val*10)) ? val + '0' : val); 
    return val;
}

// permite que se pueda ingresar en una caja de texto
// solo numeros
 function soloNumeros(e) { // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla==8) return true; // 3    
    //patron =/[0-9]\s]/; // 4    
    patron=/^([0-9])$/
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
} 

// permite que se pueda ingresar en una caja de texto
// solo numeros y separador (.)

 function soloMontos(e) { // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla==8) return true; // 3          
    patron=/^([0-9.])$/
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
} 

// remueve los espacios en blanco de una cadena
function trim(val)
{
    for(i=0; i<val.length; )
    {
	    if(val.charAt(i)==" ")
		    val=val.substring(i+1, val.length);
	    else
		    break;
    }

    for(i=val.length-1; i>=0; i=val.length-1)
    {
	    if(val.charAt(i)==" ")
		    val=val.substring(0,i);
	    else
		    break;
    }	
   return val;
}

// devuelve cantidad de caracteres
function length(str)
{
    str = trim(str);
    return str.length;
}

// redonder montos
function round(num, ndec) 
{ 
  var fact = Math.pow(10, ndec); // 10 elevado a ndec 
  /* Se desplaza el punto decimal ndec posiciones, 
    se redondea el número y se vuelve a colocar 
    el punto decimal en su sitio. */ 
  return Math.round(num * fact) / fact; 
}

function isFloat(numero)
{
    if (!/^([0-9])*[.]?[0-9]*$/.test(numero))
        return false;
    return true;
}

// cerrar modaless
function cerrarVentana()
{
    window.close();
}

function openWindowModal(frm, param, width, height) {
    var arg = 'center:yes;resizable:no;help:no;status:no;dialogWidth:' + width + ';dialogHeight:' + height + ';scroll:no;modal:yes';
    return window.showModalDialog(frm, param, arg);
}

function Finalizar()
{
   var win    = window.self;
   win.opener = window.self;
   win.close();
}

// ************************************************************************************************************************************** //
// 09-nov-2010
// ************************************************************************************************************************************** //

// resaltar fila gridview
function grillaMouseOver(src, classOver) {
    if (!src.contains(event.fromElement)) {
        src.style.cursor = 'hand';
        src.className = classOver;
    }
}

function grillaMouseOut(src, classIn) {
    if (!src.contains(event.toElement)) {
        src.style.cursor = 'default';
        src.className = classIn;
    }
}

// seleccionar un check
function gridviewSelectedCheck(spanChk, gridname) {
    var pos_ini = 1;
    var rowcount = 0;
    var parent = document.getElementById(gridname);
    rowcount = parent.rows.length;
 
    for (i = pos_ini; i < rowcount; i++) {
        var tr = parent.rows[i];
        var td = tr.childNodes[0];
        var item = td.firstChild;

        if (item.type == "checkbox") {
            if (item.checked) {
                spanChk.parentElement.parentElement.className = 'sel';
            }
            else {
                spanChk.parentElement.parentElement.className = 'def';
            }
        }
    }
}





// seleccionar un radiobutton
function CheckOtherIsCheckedByGVIDMore(spanChk, gridname) {
    var pos_ini = 1;
    var IsChecked = spanChk.checked;
    var CurrentRdbID = spanChk.id;
    var Chk = spanChk;
    var len_row = 0;

    Parent = document.getElementById(gridname);

  

    len_row = Parent.rows.length;

   

    for (i=pos_ini;i<len_row; i++) {
        var tr = Parent.rows[i];
        var td = tr.childNodes[1];
        var item = td.childNodes[1];

        if (item.type == "radio") {
            if (item.id != CurrentRdbID) {
                if (item.checked) {
                    item.checked = false;
                    //tr.className = "def";
                }
            }
            else {
                item.checked = true;
                spanChk.parentElement.parentElement.className = 'sel';
            }
        }
    }

      
    if (IsChecked){
        //spanChk.parentElement.parentElement.className = 'sel';
    }

   
}


function CheckOtherIsCheckedByGVIDMore_v2(spanChk, gridname) {
    var pos_ini = 1;
    var IsChecked = spanChk.checked;
    var CurrentRdbID = spanChk.id;
    var Chk = spanChk;
    var len_row = 0;
    var class_last;

    Parent = document.getElementById(gridname);
    len_row = Parent.rows.length;

    for (i = pos_ini; i < len_row; i++) {
        var tr = Parent.rows[i];
        var td = tr.childNodes[0];
        var item = td.firstChild;

        if (item.type == "radio") {
            if (item.id != CurrentRdbID) {
                if (item.checked) {
                    item.checked = false;
                    //tr.className = class_last;
                }
            }
        }
    }

    if (IsChecked) {
        //class_last = spanChk.parentElement.parentElement.className;
        //spanChk.parentElement.parentElement.className = 'sel';
    }
}


// TOOLBAR MANTENEDOR
function toolbarFrm(gridname, accion, url, width, heigth)
{
    var frm = "";
    var numReg = "";
    var row_count=0;
    var parm = new Array();
    var grid = document.getElementById(gridname);

    switch (accion) {
        case "delete":
            if (grid!=null){
                row_count = grid.rows.length;
                for (i = 1; i < row_count; i++) {
                    var rowsel = grid.rows[i];
                    var colsel = rowsel.cells[0];
                    var rad_sel = colsel.getElementsByTagName("input")[0];
                    var hdn_cod = colsel.getElementsByTagName("input")[1];

                    if (rad_sel != null) {
                        if (rad_sel.checked) {
                            numReg = hdn_cod.value;
                            break;
                        }
                    }
                }
            }
            
            // verificando...
            if (length(numReg) == 0) {
                alert("!Seleccione el registro!");
                return false;
            }
            
            return confirm("¿Desea eliminar?");
            

        case "edit":
            if (grid!=null){
                row_count = grid.rows.length;
                for (i = 1; i < row_count; i++) {
                    var rowsel = grid.rows[i];
                    var colsel = rowsel.cells[0];
                    var rad_sel = colsel.getElementsByTagName("input")[0];
                    var hdn_cod = colsel.getElementsByTagName("input")[1];

                    if (rad_sel != null) {
                        if (rad_sel != null && rad_sel.checked) {
                            numReg = hdn_cod.value;
                            url = url.concat("?numreg=", numReg);
                            break;
                        }
                    }
                }
            }

            // verificando...
            if (length(numReg) == 0) {
                alert("!Seleccione el registro!");
                return false;
            }

            // abrimos la ventana
            parm = openWindowModal(url, parm, width, heigth);
            return false;

        case "add":
            // abrimos la ventana
            parm = openWindowModal(url, parm, width, heigth);
            return false;
            
    }        
}

function getSelectedID(gridname)
{
    var numReg=0;
    var row_count=0;
    var grid = document.getElementById(gridname);
    if (!(grid == null)) {
        row_count = grid.rows.length;
        for (i = 1; i < row_count; i++) {
            var rowsel = grid.rows[i];
            var colsel = rowsel.cells[0];
            var rad_sel = colsel.getElementsByTagName("input")[0];
            var hdn_cod = colsel.getElementsByTagName("input")[1];

            if (rad_sel != null) {
                if (rad_sel.checked) {
                    numReg = hdn_cod.value;
                    break;
                }
            }
        }
    }

    // return
    return numReg;

}

// Selecting checkbox inside a GridView control
function SelectRowCheckbox(chkControl) {
    var isChecked = chkControl.checked;
    if (isChecked) {
        chkControl.parentElement.parentElement.className = "sel";
    } else {
        chkControl.parentElement.parentElement.className = "def";
    }
}



// Selecting multiple checkboxes inside a GridView control
function SelectAllCheckboxes(spanChk) {
    // Added as ASPX uses SPAN for checkbox
    var oItem = spanChk.children;
    var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;

    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
        //elm[i].click();
        if (elm[i].checked != xState)
            elm[i].click();
        //elm[i].checked=xState;
    }
}

// Valida que alguno de los elementos del mismo "groupname" haya sido elegido
    function validateRadioButtonList(groupname, message)
    {
        // Recogemos todos los elementos "input" de nuestra página
        var inputs = document.getElementsByTagName("input");
        var hasItems = false;
        
        // Recorremos cada uno de los elementos, 
        for (var i = 0; i < inputs.length; i++)
        {
            // y seleccionamos si hay alguno de tipo "radio"
            if (inputs[i].type == 'radio')
            {
                // Verificamos que ese RadioButton pertenece al groupname especificado
                var name = inputs[i].name;
                var isFromGroup = (name.lastIndexOf(groupname) + groupname.length ) == name.length;
                
                // En de encontrar un RadioButton con el groupname...
                if (isFromGroup)
                {   
                    // Marcamos que lo hemos encontrado
                    hasItems = true;
                    
                    // Si hay alguno checkeado, devolver true
                    if (inputs[i].checked)
                    {
                        return true;
                    }
                }
            }
        }
        
        
        if (hasItems)
        {
            // Si llegamos aquí es que hemos encontrado 
            // RadioButton's con el groupname, pero ninguno checkeado
            if (!message) message = 'Debes elegir una opción';
            alert(message);
            return false;
        }
        else
        {
            // Si llegamos aquí es que no hemos encontrado
            // ningún RadioButton con el groupname buscado
            return true;
        }
    }



    /*
        imprimir telecredito
    */

    function printDivName(divname) {
        var arg = "width=1,height=1,top=250,left=345,toolbars=no,scrollbars=no,status=no,resizable=no";
        var ficha = document.getElementById(divname);
        var ventimp = window.open("print.htm", "popimpr", arg);
        ventimp.document.write(ficha.innerHTML);
        ventimp.document.close();
        ventimp.focus();
        ventimp.print();
        ventimp.close();
        return false;
    }

    function consultarTelecreditoTrx(){
        var result = true;
        var txtfecliq = document.getElementById("dat_fecliq");
        if (!isDate(txtfecliq.value)) {
            alert("!La fecha es inválida!")
            txtfecliq.focus();
            result = false;
        }
        return result;
    }

    function cargarMovimientos() {
        var result = false;
        result = confirm("¿Desea generar los movimientos?");
        if (result) {
            result = confirm("¿Por favor confirme para iniciar con el proceso?");
        }

        /**/
        return result;
    }

    function procesarAbonos() {
        var result = false;
        result = confirm("¿Desea liquidar el Cuadre de Intercambio?");
        if (result) {
            result = confirm("¿Por favor confirme para iniciar con el proceso?");
        }
        
        /**/
        return result;
    }

    // ********************************************************************************************************//
    /* Regularizaciones manuales 25.05.11*/
    // ********************************************************************************************************//
    
    function Nuevo_Registro_Diario() {
        var codcal = 0;
        var numasi = 0;
        var row_count = 0;
        var parm = new Array();
        var urlfrm = "Frm_RegManual.aspx?codcal=";
        var grid = document.getElementById("grv_diario");

        if (!(grid == null)) {
            row_count = grid.rows.length;
            for (i=1;i< row_count; i++) {
                var rowsel = grid.rows[i];
                var rad_chksel = rowsel.cells[0].getElementsByTagName("input")[0]; // cbx_sel
                var hdn_numasi = rowsel.cells[0].getElementsByTagName("input")[2]; // num. asiento
                var hdn_codcal = rowsel.cells[0].getElementsByTagName("input")[4]; // cod. calendario

                if (rad_chksel != null) {
                    if (rad_chksel.checked) {
                        numasi = hdn_numasi.value;
                        codcal = hdn_codcal.value;
                        break;
                    }
                }
            }
        }

//        if (codcal == 0 || numasi == 0) {
//            alert("¡Seleccione el item a regularizar!");
//            return false;
//        }
        
        urlfrm = urlfrm + codcal.toString() + "&numasi=" + numasi.toString();
        parm = openWindowModal(urlfrm, parm, 55, 35);
        return false;

    }

    function Seleccionar_Cuenta(val1, val2, val3, val4) {
        var div_Cuentas = document.getElementById("divCuentas");
        
        var txt_codcta = document.getElementById("txt_codcta");
        var hdn_codmon = document.getElementById("hdn_codmon");
        var hdn_exicom = document.getElementById("hdn_exicom");
        var hdn_codcta = document.getElementById("hdn_codcta");

        txt_codcta.value = val1; // cuenta_pcge
        hdn_codmon.value = val2; // cod moneda
        hdn_exicom.value = val3; // Exigir cod comercio
        hdn_codcta.value = val4; // cod cuenta

        txt_codcta.select();
        txt_codcta.focus();
        div_Cuentas.style.display="none";
        return false;

    }

    function AgregarClick() {
        var idmoneda, idrubro;
        var txt_fecout = document.getElementById("txt_fecout"); // fecha out
        var hdn_exicom = document.getElementById("hdn_exicom"); // flg comercio
        var hdn_codmon = document.getElementById("hdn_codmon"); // flg moneda
        var hdn_codcta = document.getElementById("hdn_codcta"); // cod cuenta
        
        
        var txt_codcon = document.getElementById("txt_codcon"); // cod concepto
        var txt_codcta = document.getElementById("txt_codcta"); // cod cuenta
        var txt_codmon = document.getElementById("txt_codmon"); // cod moneda
        var txt_codrub = document.getElementById("txt_codrub"); // cod rubro
        var txt_codent = document.getElementById("txt_codent"); // cod comercio
        var txt_tipcam = document.getElementById("txt_tipcam"); // tipo cambio
        var txt_valimp = document.getElementById("txt_valimp"); // importe
        var txt_valctr = document.getElementById("txt_valctr"); // contravalor

        idmoneda = trim(txt_codmon.value);
        idmoneda = idmoneda.toUpperCase();
        idrubro = trim(txt_codrub.value);
        idrubro = idrubro.toUpperCase();

        // Fecha de outgoing...
        if (length(txt_fecout.value) == 0) {
            alert("¡Ingrese la fecha Out!");
            txt_fecout.select();
            txt_fecout.focus();
            return false;
        }

        // id concepto
        if (length(txt_codcon.value) == 0) {
            alert("¡Ingrese el concepto!");
            txt_codcon.select();
            txt_codcon.focus();
            return false;
        }

        // id cuenta
        if (length(txt_codcta.value) == 0) {
            alert("¡Ingrese la cuenta!");
            txt_codcta.select();
            txt_codcta.focus();
            return false;
        }

        // cod cuenta
        if (trim(hdn_codcta.value) == "") {
            alert('¡Debe seleccionar la cuenta!');
            txt_codcta.select();
            txt_codcta.focus();
            return false;
        }

        // id moneda
        if (length(idmoneda) == 0) {
            alert('¡Ingrese la moneda!');
            txt_codmon.select();
            txt_codmon.focus();
            return false;
        }


        // validando la moneda...
        // 12.oct.2011
        if (trim(hdn_codmon.value) == "") {
            alert('¡La cuenta no tiene moneda. Por favor seleccione otra cuenta!');
            txt_codmon.select();
            txt_codmon.focus();
            return false;
        }
      
        if (trim(hdn_codmon.value) == "A") {
            if (!(idmoneda == "S" || idmoneda == "D")) {
                alert("¡Solo se permite S/D!");
                txt_codmon.select();
                txt_codmon.focus();
                return false;
            }
        } else {
            if (!(idmoneda == trim(hdn_codmon.value))) {
                alert("¡La moneda es inválida!");
                txt_codmon.select();
                txt_codmon.focus();
                return false;
            }
        }

        // id rubro contable
        if (!(idrubro == "D" || idrubro == "H")) {
            alert("¡El rubro es inválido!")
            txt_codrub.select();
            txt_codrub.focus();
            return false;
        }

        // importe
        if (!(parseFloat(txt_valimp.value) > 0)) {
            alert('¡El importe es inválido!');
            txt_valimp.select();
            txt_valimp.focus();
            return false;
        }

        // contravalor
        if (length(txt_valctr.value)== 0) {
            alert('¡Ingrese el contravalor!');
            txt_valctr.focus();
            return false;
        }
        
        if (length(txt_valctr.value) > 0) {
            if (!isFloat(trim(txt_valctr.value))) {
                alert('¡El contravalor es inválido!');
                txt_valctr.select();
                txt_valctr.focus();
                return false;
            }
        }
        
        // exigir comercio
        if (trim(hdn_exicom.value) == "1") {
            if (length(txt_codent.value) == 0) {
                alert("Ingrese el codigo de comercio");
                txt_codent.select();
                txt_codent.focus();
                return false;
            }
        }

        // es válido!
        return confirm("¿Desea agregar el item?");

    }

    // ********************************************************************************************************//
    // ********************************************************************************************************//

    function Ir_Operaciones() {
        var urlstr = document.URL;
        var codtar = getSelectedID("grv_Tareas");
        if (codtar == 0) {
            alert("¡Seleccione un proceso a consultar!");
            return false;
        }

        urlstr = urlstr.replace("Frm_Tarea", "Frm_ConsultaOperativa");
        urlstr = urlstr + "?codtar=" + codtar.toString();
        
        //alert(urlstr);
        document.location = urlstr;
        return false;

    }

    function VerDinamicaTRX(value) {
        var frm="Frm_DetalleOperativoContable.aspx?codope="+value
        openWindowModal(frm, "", 50, 35);
        return false;
    }
    
    function formatNumber(num,prefix){
        prefix = prefix || '';
        num +='';
        var splitStr = num.split('.');
        var splitLeft = splitStr[0];
        var splitRight = splitStr.length > 1 ? '.' + splitStr[1] : '';
        var regx = /(\d+)(\d{3})/;
        while (regx.test(splitLeft)) {
        splitLeft = splitLeft.replace(regx, '$1' + ',' + '$2');
    }
    return prefix + splitLeft + splitRight;
    }

    function unformatNumber(num) {
    return num.replace(/([^0-9\.\-])/g,'')*1;
    }

    function formato_numero(numero, decimales, separador_decimal, separador_miles) { // v2007-08-06
        numero = parseFloat(numero);
        if (isNaN(numero)) {
            return "";
        }

        if (decimales !== undefined) {
            // Redondeamos
            numero = numero.toFixed(decimales);
        }

        // Convertimos el punto en separador_decimal
        numero = numero.toString().replace(".", separador_decimal !== undefined ? separador_decimal : ",");

        if (separador_miles) {
            // Añadimos los separadores de miles
            var miles = new RegExp("(-?[0-9]+)([0-9]{3})");
            while (miles.test(numero)) {
                numero = numero.replace(miles, "$1" + separador_miles + "$2");
            }
        }

        return numero;
    }

