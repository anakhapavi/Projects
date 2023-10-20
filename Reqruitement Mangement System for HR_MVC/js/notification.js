    $(document).ready(function () {
        $("#contactForm").submit(function (event) {
            event.preventDefault(); 


            setTimeout(function () {
                showNotification("Your message has been sent. Thank you!");
            }, 1000); 

            clearFormFields();
        });

    function showNotification(message) {
            var notificationElement = $("#notification");
    notificationElement.text(message).addClass("success").fadeIn();
        setTimeout(function () {
        notificationElement.fadeOut();
            }, 5000); 
        }

    function clearFormFields() {
        $("#contactForm")[0].reset();
        }
    });
