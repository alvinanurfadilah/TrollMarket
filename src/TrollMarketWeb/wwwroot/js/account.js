(() => {
    const url = "http://localhost:8081/api/v1/account";

    // #region Login
    let letComment = () => {
        let username = document.querySelector(".username").value;
        let password = document.querySelector(".password").value;
        let role = document.querySelector(".role").value;
        console.log(username);

        return { username, password, role };
    };

    let tokens = () => {
        let request = new XMLHttpRequest();
        request.open("POST", url);
        request.setRequestHeader("Content-Type", "application/json");
        request.send(JSON.stringify(letComment()));
        request.onload = () => {
            console.log(request.response);
            // let statusRequest = request.status;
            localStorage.setItem("token", request.response);
        };
    };

    let login = () => {
        let getButton = document.querySelector(".login-button");
        getButton.addEventListener("click", (event) => {
            // event.preventDefault();
            tokens();
        });
    };
    // #endregion

    login();
})();
