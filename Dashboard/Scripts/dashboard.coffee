@getFormSubmissions = ->
    $.getJSON '/api/form', (forms) ->
        gauge.refresh forms.length    
        $('.lastForm').html forms[0].Form.Name

@getVisitors = ->
    gapi.client.analytics.data.realtime.get
        ids: 'ga:23580935'
        dimensions: 'rt:pageTitle'
        metrics: 'rt:activeUsers'
    .execute (response) ->
        response.rows.sort (a, b) -> return(b[1] - a[1])
        $('#website-pages').html('<tr><th>Page</th><th style="width:10%">Visitors</th></tr>')
        $('#website-pages').append('<tr><td>' + response.rows[a][0] + '</td><td>' + response.rows[a][1] + '</td></tr>') for a in [0..9]
        total = response.totalsForAllResults['rt:activeUsers']
        visitorsGauge.refresh total

@getIntranetVisitors = ->
    gapi.client.analytics.data.realtime.get
        ids: 'ga:23109414'
        dimensions: 'rt:pageTitle'
        metrics: 'rt:activeUsers'
    .execute (response) ->
        response.rows.sort (a, b) -> return(b[1] - a[1])
        $('#intranet-pages').html('<tr><th>Page</th><th style="width:10%">Visitors</th></tr>')
        $('#intranet-pages').append('<tr><td>' + response.rows[a][0] + '</td><td>' + response.rows[a][1] + '</td></tr>') for a in [0..9]
        total = response.totalsForAllResults['rt:activeUsers']
        intranetGauge.refresh total

@buildGraph = ->
    @gauge = new JustGage
        id: 'formsGauge'
        value: 0
        title: 'Form submissions'
        label: 'today'

    @visitorsGauge = new JustGage
        id: 'visitorsGauge'
        value: 0
        max: 300
        title: 'Website users'
        label: 'active right now'

    @intranetGauge = new JustGage
        id: 'intranetGauge'
        value: 0
        max: 300
        title: 'Intranet users'
        label: 'active right now'

$ ->
    buildGraph()
    getFormSubmissions()
    setInterval getFormSubmissions, 10000 #10 second delay between calls