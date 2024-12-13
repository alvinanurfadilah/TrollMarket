(() => {
    const url = 'http://localhost:8081/api/v1/cart'

    let productId;
    //#region POST
    let insertCart = () => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';

        let qty = document.querySelector('.qty').value;
        let accountId = document.querySelector('.accountId').textContent;
        let shipmentId = document.querySelector('.shipment-dropdown').value;

        let getClose = document.querySelector('.close-cart');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });

        return {productId, qty, shipmentId, accountId}
    }

    let sendCart = () => {
        let request = new XMLHttpRequest();
        request.open('POST', url);
        request.setRequestHeader("Content-Type", "application/json");
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.send(JSON.stringify(insertCart()));
        request.onload = () => {
            console.log(request.response);
            location.reload();
        }
    }

    let getDropdown = () => {
        let request = new XMLHttpRequest();
        request.open('GET', url);
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.onload = () => {
            let data = JSON.parse(request.response);
            let shipments = data.shipments;

            let selectShipment = document.querySelector('.shipment-dropdown');

            shipments.forEach((shipment) => {
                let option = new Option(shipment.text, shipment.value);
                selectShipment.options.add(option);
            });
        }
        request.send();
    }

    let submit = () => {

        let getSubmit = document.querySelector('.insert-cart');
        getSubmit.addEventListener('click', (event) => {
            event.preventDefault();
            sendCart();
        });
    }

    let getAddtoCart = () => {
        let getButtons = document.querySelectorAll('.add');
        getButtons.forEach(product => {
            product.addEventListener('click', (event) => {
                event.preventDefault();
                productId = product.getAttribute("value");
                insertCart();
            });
        });
    }
    //#endregion

    //#region Detail Info
    let bindingDetail = (detail) => {
        let modal = document.querySelector('.modal-detail');
        modal.style.display = 'flex';

        let name = document.querySelector('#name');
        let category = document.querySelector('#category');
        let description = document.querySelector('#description');
        let price = document.querySelector('#price');
        let sellerName = document.querySelector('#sellerName');

        console.log(detail);
        name.textContent = detail.name;
        category.textContent = detail.categoryName;
        description.textContent = detail.description;
        price.textContent = detail.price;
        sellerName.textContent = detail.sellerName;

        let getClose = document.querySelector('.close-cart-detail');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });
    }

    let detailInfo = (button) => {
        let row = button.parentElement.parentElement.parentElement;
        console.log(row);
        let getId = row.querySelector('td:nth-child(2)').textContent;
        console.log(getId);

        let request = new XMLHttpRequest();
        request.open('GET', `${url}/detail/${getId}`);
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.send();
        request.onload = () => {
            let detailInfo = JSON.parse(request.response);
            bindingDetail(detailInfo);
        }
    }

    let getButtonDetail = () => {
        let buttonDetail = document.querySelectorAll('.detail-info');
        buttonDetail.forEach(detail => {
            detail.addEventListener('click', (event) => {
                event.preventDefault();
                detailInfo(event.target);
            })
        })
    }
    //#endregion

    getButtonDetail();
    getDropdown();
    submit();
    getAddtoCart();
})();