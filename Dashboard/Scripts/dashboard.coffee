@getFormSubmissions = ->
    $.getJSON '/api/form', (forms) ->
        gauge.refresh forms.length    
        $('.lastForm').html forms[0].Form.Name

@buildGraph = ->
    @gauge = new JustGage
        id: 'formsGauge'
        value: 0
        title: 'Form Submissions'
        label: 'today'

$('#start').click -> 
    getFormSubmissions()
    setInterval getFormSubmissions, 10000 #10 second delay between calls
    $(this).hide()

$ ->
    buildGraph()