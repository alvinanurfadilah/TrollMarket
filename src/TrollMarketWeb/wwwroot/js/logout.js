(() => {
    let logout = () => {
        let getButton = document.querySelector(".logout-button");
        getButton.addEventListener("click", (event) => {
            // event.preventDefault();
            localStorage.removeItem("token");
            // location.reload();
        });
    };

    logout();
})();