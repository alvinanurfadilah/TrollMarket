(() => {
    const url = 'http://localhost:8081/api/v1/shipment';

    //#region PUT
    let updateShipment = () => {
        let id = document.querySelector('.id').value;
        let name = document.querySelector('.name').value;
        let cost = document.querySelector('.price').value;
        let isService = document.querySelector('.service').checked;

        let modal = document.querySelector('.modal');
        let getClose = document.querySelector('.close-shipment');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {id, name, cost, isService};
    }

    let sendShipmentUpdate = () => {
        let request = new XMLHttpRequest();
        request.open('PUT', `${url}`);
        request.setRequestHeader("Content-Type", "application/json");
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.send(JSON.stringify(updateShipment()));
        request.onload = () => {
            console.log(request.response);
            location.reload();
        }
    }

    let submitUpdate = () => {
        let button = document.querySelector('.update-shipment');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            sendShipmentUpdate();
        });
    }

    let bindingShipment = (shipment) => {
        console.log(shipment);
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getSubmit = document.querySelector('.insert-shipment');
        getSubmit.style.display = 'none';
        
        let getId = document.querySelector('.id');
        let getName = document.querySelector('.name');
        let getPrice = document.querySelector('.price');
        let getService = document.querySelector('.service');

        getId.value = shipment.id;
        getName.value = shipment.name;
        getPrice.value = shipment.cost;
        getService.checked = shipment.isService;
    }

    let getShipment = (id) => {
        let request = new XMLHttpRequest();
        request.open('GET', `${url}?id=${id}`);
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.send();
        request.onload = () => {
            let shipment = JSON.parse(request.response);

            bindingShipment(shipment);
        }
    }

    let getUpdateShipment = () => {
        let getButtons = document.querySelectorAll('.shipment-edit');
        getButtons.forEach(shipment => {
            shipment.addEventListener('click', (event) => {
                event.preventDefault();
                getShipment(shipment.getAttribute("value"));
            });
        });
    }
    //#endregion

    //#region POST
    let insertShipment = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';
        let getSubmit = document.querySelector('.update-shipment');
        getSubmit.style.display = 'none';

        let name = document.querySelector('.name').value;
        let cost = document.querySelector('.price').value;
        let isService = document.querySelector('.service').checked;

        let getClose = document.querySelector('.close-shipment');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {name, cost, isService};
    }

    let sendShipment = () => {
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.send(JSON.stringify(insertShipment()));
        request.onload = () => {
            console.log(request.response);
            location.reload();
        }
    }

    let submit = () => {
        let getSubmit = document.querySelector('.insert-shipment');
        getSubmit.addEventListener('click', (event) => {
            event.preventDefault();
            sendShipment();
        });
    }

    let getAddNewShipment = () => {
        let button = document.querySelector('.add');
        button.addEventListener('click', (event) => {
            event.preventDefault();
            insertShipment();
        });
    }

    //#endregion

    submitUpdate();
    getUpdateShipment();
    submit();
    getAddNewShipment();
})();