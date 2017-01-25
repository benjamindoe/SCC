var api = 'https://maps.googleapis.com/maps/api/geocode/json?latlng=';
var GoogleApiKey = 'AIzaSyBsTh-KSE811phm0nZiIH_ifnPpuYHTytc';
function mapPopup(params) {
    console.log(params);
    var backdrop = $('<div>');
    backdrop.addClass('popup__backdrop');
    var iframe = $('<iframe>');
    var latlng = params.data.lat + ',' + params.data.lng;
    iframe.attr('src', 'https://www.google.com/maps/embed/v1/directions?origin='+latlng+'&destination='+params.data.dest+'&key='+GoogleApiKey);
    iframe.addClass('popup__map');
    backdrop.append(iframe);
    $('.content-wrapper').append(backdrop);
    backdrop.click(function(){$(this).remove()});
}
$(function() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            var url = api + pos.lat + ',' + pos.lng + '&result_type=country&key='+ GoogleApiKey;
            var ajaxData;
            $.get(url, function(data) {
                ajaxData = data.results[0].formatted_address;
                $('li.flights__flight').each(function (event) {
                    var startAirport = $(this).find(".start_city");
                    if(startAirport.data('country') === ajaxData)
                    {
                        var airport = startAirport.data('airport');
                        $(this).append('<span><a href="#"id="mapsPopupLnk">Drive to '+airport+' from your location</a></span>');
                        $('#mapsPopupLnk').click({lat: pos.lat, lng: pos.lng, dest: airport}, mapPopup);
                    }
                });
            });
        });
    } else 
    {
        alert("ERROR");
    }
});
