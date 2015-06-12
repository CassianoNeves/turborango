
    var keyCodesPermitidos = new Array(9, 39, 37, 188, 189);
    $("input.number").keydown(function (event) {
        //alert(event.keyCode);
        var tecla = (window.event) ? event.keyCode : event.which;
        if ($.inArray(tecla, keyCodesPermitidos) != -1) {
            return true;
        }
        if ((tecla > 47 && tecla < 58))
            return true;
        else {
            if (tecla != 8)
                return false;
            else
                return true;
        }
    });

    function mascara(o, f) {
        v_obj = o
        v_fun = f
        setTimeout("execmascara()", 1)
    }
    function execmascara() {
        v_obj.value = v_fun(v_obj.value)
    }
    function mtel(v) {
        v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
        v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
        v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
        return v;
    }
