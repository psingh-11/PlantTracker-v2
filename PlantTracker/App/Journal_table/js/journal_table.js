$(document).ready(function () {

    var table = $('#journalTable').DataTable(
        {
            // test

            "lengthMenu": [15, 25, 50, 75, 100],
            "columnDefs": [
                { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3] },
                {
                    "targets": 1,
                    "orderable": false,
                    render: function (data) {
                        return "<img height='42' width='42'  class='img-circle' src='" + data + "'>";
                    }
                },
                {
                    "targets": 0,
                    "visible": false
                }
            ],

            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": true,
            "ajax":
            {
                "url": toolbox.GetLocationUrl() + "/Journal/GetData",
                "type": "POST",
                "dataType": "JSON"
            },
            "columns": [
                { "data": "JournalId" },
                { "data": "Image" },
                { "data": "Name" },
                { "data": "Date" }
            ]

        });

    $('#journalTable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        document.location.href = "/journalview/journalDisplay?journalid=" + data.JournalId;
    });
});