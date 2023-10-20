 $(document).ready(function () {
        $(".edit-button").click(function () {
            var appId = $(this).data("appid");
            var currentStatus = $(this).data("status");

            $("#editAppId").val(appId);
            $("#editStatus").val(currentStatus);

            $("#statusEditModal").modal("show");
        });

        $("#saveStatus").click(function () {
            var appId = $("#editAppId").val();
            var newStatus = $("#editStatus").val();

            // Send an AJAX request to update the status
            $.ajax({
                url: '/Application/UpdateStatus',
                method: 'POST',
                data: { appId: appId, newStatus: newStatus },
                success: function (data) {
                    if (data.success) {
                       
                        $("#statusEditModal").modal("hide");
                    } else {
                        alert('Status update failed.');
                    }
                },
                error: function () {
                    // Handle error if AJAX request fails
                    alert('An error occurred.');
                }
            });
        });
    });
