$(function () {

var substringMatcher = function(strs) {
    return function findMatches(q, cb) {
        var matches, substringRegex;
        // an array that will be populated with substring matches
        matches = [];
        // regex used to determine if a string contains the substring `q`
        substrRegex = new RegExp(q, 'i');
        // iterate through the pool of strings and for any string that
        // contains the substring `q`, add it to the `matches` array
        $.each(strs, function(i, str) {
            console.log(str);
            if (substrRegex.test(str)) {
                matches.push(str);	
            }
        });
        // strs.forEach(function(i){
        // 	console.log(i.nome);
        // 	if (substrRegex.test(i.nome)) {
        //    		matches.push(i.nome);
        //    	}
        // });
        cb(matches);
    };
};
var filmes = [];
$.ajax({
    url: '/Reservas/Restaurantes',
    type: 'GET'
}).done(function (res) {
    res.restaurantes.forEach(function(i){
    filmes.push(i.Nome);
});
    // filmes = res;
    // filmes.forEach(function(i){
    // 	console.log(i.nome);
    // });
});
// var states = ['Alabama', 'Alaska', 'Arizona', 'Arkansas', 'California',
// 'Colorado', 'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii',
// 'Idaho', 'Illinois', 'Indiana', 'Iowa', 'Kansas', 'Kentucky', 'Louisiana',
// 'Maine', 'Maryland', 'Massachusetts', 'Michigan', 'Minnesota',
// 'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
// 'New Jersey', 'New Mexico', 'New York', 'North Carolina', 'North Dakota',
// 'Ohio', 'Oklahoma', 'Oregon', 'Pennsylvania', 'Rhode Island',
// 'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
// 'Virginia', 'Washington', 'West Virginia', 'Wisconsin', 'Wyoming'
// ];
$('#the-basics .typeahead').typeahead({
    hint: true,
    highlight: true,
    minLength: 1
},
{
    name: 'filmes',
    source: substringMatcher(filmes)
});
$('.typeahead').bind('typeahead:select', function(ev, suggestion) {
    $.ajax({
        url: '/Reservas/Restaurante/' + suggestion,
        type: 'GET'
    }).done(function(res){
        location.href = "/Reservas/Create/" + res.restaurante[0].Id;
    });
});
});