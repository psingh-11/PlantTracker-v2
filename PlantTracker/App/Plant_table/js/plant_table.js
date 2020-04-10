$(document).ready(function () {

    var table = $('#plantTable').DataTable(
        {
            // test

            "lengthMenu": [15, 25, 50, 75, 100],           
            "columnDefs": [
                {
                    "width": "2%", "targets": [4],
                },
                { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3, 4, 5, 6] },
                {
                    "targets": 1,
                    "orderable":false,
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
                    "url": toolbox.GetLocationUrl() + "/Plant/GetData",
                    "type": "POST",
                    "dataType": "JSON"
                },
            "columns": [
                { "data": "PlantId" },
                { "data": "Image" },
                { "data": "Name" },
                { "data": "Type" },
                { "data": "Species" },
                { "data": "Count" },
                { "data": "Description" }
            ]
            
        });

    $('#plantTable tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        document.location.href = "/plantview/PlantDisplay?plantid=" + data.PlantId;
        //plantTableService.GetDetails(data.PlantId);

    });

    $(".open-AddBookDialog").click(function () {
        $('#bookId').val($(this).data('id'));
        $("#addBookDialog").modal("show");
    });




});