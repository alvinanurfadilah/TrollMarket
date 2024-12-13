(() => {
    const url = 'http://localhost:8081/api/v1/account';

    //#region Balance
    let updateBalance = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let balance = document.querySelector('#balance').value;
        let getButtons = document.querySelector('.balance-edit');
        let username = getButtons.getAttribute("value");

        let getClose = document.querySelector('.close-account');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {username, balance}
    }

    let sendBalanceUpdate = () => {
        let request = new XMLHttpRequest();
        request.open('PATCH', `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.send(JSON.stringify(updateBalance()));
        request.onload = () => {
            console.log(request.response);
            location.reload();
        }
    }

    let submitUpdate = () => {
        let button = document.querySelector('.update-balance');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            sendBalanceUpdate();
        });
    }

    let getUpdateBalance = () => {
        let getButtons = document.querySelector('.balance-edit');
        getButtons.addEventListener('click', (event) => {
            event.preventDefault();
            updateBalance(getButtons.getAttribute("value"));
        });
    }
    //#endregion

    submitUpdate();
    getUpdateBalance();
})();