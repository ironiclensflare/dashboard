@getFormSubmissions = ->
    $.getJSON '/api/form', (forms) ->
        $('.bigNum').html forms.length
        $('.lastForm').html forms[0].Form.Name

$('#start').click -> 
    getFormSubmissions()
    setInterval getFormSubmissions, 30000