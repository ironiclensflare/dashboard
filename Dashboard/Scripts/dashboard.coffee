@getFormSubmissions = ->
    $.getJSON '/api/form', (forms) ->
        gauge.refresh forms.length    
        $('.lastForm').html forms[0].Form.Name

@getVisitors = ->
    gapi.client.analytics.data.realtime.get
        ids: 'ga:81750023'
        metrics: 'rt:activeUsers'
    .execute (response) ->
        total = response.totalsForAllResults['rt:activeUsers']
        visitorsGauge.refresh total

@buildGraph = ->
    @gauge = new JustGage
        id: 'formsGauge'
        value: 0
        title: 'Form submissions'
        label: 'today'

    @visitorsGauge = new JustGage
        id: 'visitorsGauge'
        value: 0
        title: 'Website users'
        label: 'active right now'

$ ->
    buildGraph()
    getFormSubmissions()
    setInterval getFormSubmissions, 10000 #10 second delay between calls