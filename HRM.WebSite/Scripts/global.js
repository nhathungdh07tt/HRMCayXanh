function setDate(elementId, date) {
    var _date;
    if (!date) {
        _date = new Date();      
    } else {
        _date = new Date(date);        
    }
    const offset = new Date().getTimezoneOffset() / 60;
    _date.setHours(_date.getHours() - offset);
    document.getElementById(elementId).valueAsDate = _date;
   
}