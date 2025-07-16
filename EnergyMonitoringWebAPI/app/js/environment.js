(function (env) {  

    //Public property
    // env.server = '';
     //env.server = 'http://localhost:56447';
     env.server = 'http://ilzmsenergy1';
    
})(window.env = window.env || {})


function getDateNow() {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear()+"-"+(month)+"-"+(day) ;

    return today;
}

function formatDate(date) {
    var now = new Date(date);

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear()+"-"+(month)+"-"+(day) ;

    return today;
}

