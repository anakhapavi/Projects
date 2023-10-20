
    function hrLogout() {
        window.location.href = "/Logindetails/Login";  
    }

    function candidateLogout() {
        window.location.href = "/CandidateLogin/Login";
    }

    document.getElementById("hrLogoutLink").addEventListener("click", function (e) {
        e.preventDefault(); 
    hrLogout(); 
    });

    document.getElementById("candidateLogoutLink").addEventListener("click", function (e) {
        e.preventDefault(); 
    candidateLogout(); 
    
