(() => {
    const url = 'http://localhost:8081/api/v1/merchandise';

    let bindingDetail = (detail) => {
        let modal = document.querySelector('.modal');
        modal.style.display = 'flex';

        let name = document.querySelector('#name');
        let category = document.querySelector('#categoryName');
        let description = document.querySelector('#description');
        let price = document.querySelector('#price');
        let discontinue = document.querySelector('#discontinue');
        
        name.textContent = detail.name;
        category.textContent = detail.categoryName;
        description.textContent = detail.description;
        price.textContent = detail.price;
        discontinue.textContent = detail.discontinue;

        let getClose = document.querySelector('.close-merchandise');
        getClose.addEventListener('click', (event) => {
            event.preventDefault();
            modal.style.display = 'none';
        });
    }

    let detailInfo = (button) => {
        let row = button.parentElement.parentElement.parentElement;
        let getId = row.querySelector('td:nth-child(2)').textContent;

        let request = new XMLHttpRequest();
        request.open('GET', `${url}/info/${getId}`);
        request.setRequestHeader("Authorization", `Bearer ${localStorage.getItem('token')}`);
        request.send();
        request.onload = () => {
            let detailInfo = JSON.parse(request.response);
            bindingDetail(detailInfo);
        }
    }

    let getButtonInfo = () => {
        let buttonDetail = document.querySelectorAll('.info');
        buttonDetail.forEach(detail => {
            detail.addEventListener('click', (event) => {
                event.preventDefault();
                detailInfo(event.target);
            })
        })
    }

    getButtonInfo();
})();