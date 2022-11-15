const logo = document.querySelector("#person-logo");
const infoBtn = document.querySelector("#info-btn");
const deleteBtn = document.querySelector("#delete-btn");
const logout = document.querySelector('#logout-btn');
const username = document.querySelector('#username');
const editBtn = document.querySelector('#edit-btn');
const addBtn = document.querySelector('#add-btn');
const infoBox = document.querySelector('.info-box');
const addInfo = document.querySelector('.add-info');
const addForm = document.querySelector('#add-form');
const infoTab = document.querySelector('.info-tab');
const deleteUserTab = document.querySelector('.delete-user');
let user;

window.onload = () => {

    SetUser();

};


function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

const SetUser = () => {
    let userString = sessionStorage.getItem('user');

    if (userString) {
        user = JSON.parse(userString);
        SetButtons();
        return;
    }

    logout.addEventListener('click', () => {
        window.location = 'index.html';
    })
    toastr.error('Please login');
}

const SetButtons = () => {

    const parsedToken = parseJwt(user.token);

    if (parsedToken.Role !== "Admin") {
        deleteBtn.style.display = 'none';
    } else {
        deleteBtn.style.display = 'flex';
    }


    logo.addEventListener('click', () => {
        window.location = "person-information.html";
    });

    logout.addEventListener('click', () => {
        sessionStorage.clear();
        window.location = 'index.html';
    });

    infoBtn.addEventListener('click', () => {

        if (infoTab.style.display == 'none') {
            deleteUserTab.style.display = 'none';
            getPersonInfo();
            addBtn.style.display = 'block';
            infoTab.style.display = 'flex';
            return;
        }
        addInfo.style.display = 'none';
        infoTab.style.display = 'none';
    });

    editBtn.addEventListener('click', () => editPersonInfo())

    addBtn.addEventListener('click', () => {
        addInfo.style.display = 'flex';
        addForm.style.display = 'flex';
        addForm.style.flexDirection = 'column';
        addForm.style.alignItems = 'flex-end';

        addForm.addEventListener('submit', event => {
            event.preventDefault();

            const formData = new FormData(addForm);

            addPersonInfo(formData);

            addInfo.style.display = 'none';
            addBtn.style.display = 'none';
            infoTab.style.display = 'none';
        })

    });

    deleteBtn.addEventListener('click', () => {

        if (deleteUserTab.style.display == 'none') {
            deleteUserTab.style.display = 'flex';
            addInfo.style.display = 'none';
            infoTab.style.display = 'none';
        } else {
            deleteUserTab.style.display = 'none';
            return;
        }
        removePersonAccount();
    });
}


const getPersonInfo = () => {
    editBtn.style.display = 'Block';

    fetch(`https://localhost:7021/PersonInformation/GetPersonByUserId?userId=${user.userId}`, {
        method: 'GET',
        headers: {
            'Authorization': 'Bearer ' + user.token
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data.errorMessage) {
                editBtn.style.display = 'none';
                toastr.error(data.errorMessage);
            }
            else {
                infoBox.style.display = 'flex';
                addBtn.style.display = 'none';

                const image = document.querySelector('#image').querySelector('img');
                image.src = "data:image/png; base64," + data.image.imageBytes;
                document.querySelector('#firstName').value = data.firstName;
                document.querySelector('#lastName').value = data.lastName;
                document.querySelector('#personalNumber').value = data.personalNumber;
                document.querySelector('#phoneNumber').value = data.phoneNumber;
                document.querySelector('#email').value = data.email;
                document.querySelector('#city').value = data.address.city;
                document.querySelector('#street').value = data.address.street;
                document.querySelector('#buildingNumber').value = data.address.buildingNumber;

                if (data.address.flatNumber) {
                    document.querySelector('#flatNumber').value = data.address.flatNumber;
                }

                showInfoFields();
            }
        })
        .catch(error => {
            console.log(error);
        });
}

const editPersonInfo = () => {
    editBtn.style.display = 'none';
    const refreshBtn = document.createElement('Button');
    refreshBtn.innerHTML = "Refresh";

    const image = document.querySelector('#image').querySelector('img');
    const firstName = document.querySelector('#firstName');
    const lastName = document.querySelector('#lastName');
    const personalNumber = document.querySelector('#personalNumber');
    const phoneNumber = document.querySelector('#phoneNumber');
    const email = document.querySelector('#email');
    const city = document.querySelector('#city');
    const street = document.querySelector('#street');
    const buildingNumber = document.querySelector('#buildingNumber');
    const flatNumber = document.querySelector('#flatNumber');

    updateImage(image);
    updateFirstName(firstName);
    updateLastName(lastName);
    updatePersonalNumber(personalNumber);
    updatePhoneNumber(phoneNumber);
    updateEmail(email);
    updateAddressCity(city);
    updateAddressStreet(street);
    updateBuildingNumber(buildingNumber);
    updateFlatNumber(flatNumber);

    infoBox.append(refreshBtn);
    refreshBtn.addEventListener('click', () => {
        window.location.reload();
        refreshBtn.remove();
    });
}

const addPersonInfo = (formData) => {
    fetch(`https://localhost:7021/PersonInformation/CreatePerson?userId=${user.userId}`, {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + user.token,
        },
        body: formData
    })
        .then(res => res.json())
        .then(data => {
            if (data.errorMessage) {
                toastr.warning(data.errorMessage);
            }
            if (data.message) {
                toastr.sussess(data.message);
            }
        })
        .catch(err => console.log(err));
}

const updateImage = (imageBox) => {

    const form = document.createElement('form');
    const imgInput = document.createElement('input');
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    imgInput.setAttribute("type", "file");
    imgInput.setAttribute('name', 'personImage');
    imgInput.setAttribute('id', 'img-input')
    form.setAttribute('id', 'image-form')
    imageBox.style.display = 'none';

    infoBox.prepend(form);
    form.append(imgInput);
    form.append(saveBtn);

    form.addEventListener('submit', (event) => {
        event.preventDefault();
        const form = document.querySelector('#image-form');

        const formData = new FormData();

        formData.append('PersonImage', imgInput.files[0]);

        fetch(`https://localhost:7021/PersonInformation/UpdateImage?userId=${user.userId}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            },
            body: formData
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Image Updated!');

                }
            })
    });

}

const updateFirstName = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const firstName = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdateFirstName?userId=${user.userId}&firstName=${firstName}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('First Name Updated!');
                }
            })
    });
}

const updateLastName = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const lastName = value.value;
        fetch(`https://localhost:7021/PersonInformation/UpdateLastName?userId=${user.userId}&lastName=${lastName}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Last Name Updated!');
                }
            })
    });
}

const updatePersonalNumber = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const personalNumber = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdatePersonalNumber?userId=${user.userId}&personalNumber=${personalNumber}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Personal Number Updated!');
                }
            })
    });
}

const updatePhoneNumber = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const phoneNumber = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdatePhoneNumber?userId=${user.userId}&phoneNumber=${phoneNumber}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Phone Number Updated!');
                }
            })
    });
}

const updateEmail = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const email = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdateEmail?userId=${user.userId}&email=${email}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Email Updated!');
                }
            })
    });
}

const updateAddressCity = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const city = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdateAddressCity?userId=${user.userId}&addressCity=${city}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('City Updated!');
                }
            })
    });
}

const updateAddressStreet = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const street = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdateAddressStreet?userId=${user.userId}&addressStreet=${street}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Street Updated!');
                }
            })
    });
}

const updateBuildingNumber = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const buildingNumber = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdateAddressBuildingNumber?userId=${user.userId}&buildingNumber=${buildingNumber}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Building number Updated!');
                }
            })
    });
}

const updateFlatNumber = (value) => {
    value.readOnly = false;
    const saveBtn = document.createElement('input');

    saveBtn.value = 'Save';
    saveBtn.type = 'submit';

    value.parentElement.append(saveBtn);

    saveBtn.addEventListener('click', () => {
        const flatNumber = value.value;

        fetch(`https://localhost:7021/PersonInformation/UpdateAddressFlatNumber?userId=${user.userId}&flatNumber=${flatNumber}`, {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => {
                if (resp.status == '200') {
                    toastr.success('Flat Number Updated!');
                }
                if (resp.status == '400') {
                    let data = resp.json();
                    toastr.warning(data.errorMessage);
                }
            })
    });
}

const removePersonAccount = () => {
    const deleteForm = deleteUserTab.querySelector('form');

    deleteForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const userId = deleteForm.querySelector('#userId').value;

        fetch(`https://localhost:7021/Admin/RemoveUserAccount?userId=${userId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + user.token,
            }
        }).
            then(resp => resp.json())
            .then(data => {
                if (data.errorMessage) {
                    toastr.error(data.errorMessage);
                }
                if (data.message) {
                    toastr.success(data.message);
                }
            })
    });
}

const showInfoFields = () => {
    let imageField = infoTab.querySelector('#image');
    let fields = infoTab.querySelectorAll('.input');

    imageField.style.display = 'flex';

    fields.forEach(element => {
        element.style.display = 'flex';
    });

}