const registerBtn = document.querySelector('#register-btn');
const loginBtn = document.querySelector('#login-btn');
const registerForm = document.querySelector('#register-form');
const loginForm = document.querySelector('#login-form');
const registerBox = document.querySelector('#register-box');
const loginBox = document.querySelector('#login-box');
const logo = document.querySelector("#person-logo");
const userKey = 'user';
let user = {};

window.onload = () => {
    setButtons();
}

const setButtons = () => {
    registerBtn.addEventListener('click', () => {
        registerBox.style.display = 'block';
        loginBox.style.display = 'none';
        registration();
    });

    loginBtn.addEventListener('click', () => {
        registerBox.style.display = 'none';
        loginBox.style.display = 'block';
        logIn();
    });

    logo.addEventListener('click', () => {
        window.location = "index.html";
    })
}

const registration = () => {

    registerForm.addEventListener('submit', (event) => {
        event.preventDefault();
        const name = registerForm.querySelector('#name-inp-reg').value;
        const password = registerForm.querySelector('#pass-inp-reg').value;
        const verifyPassword = registerForm.querySelector('#verify-pass-inp-reg').value;

        fetch("https://localhost:7021/Authorization/Registration", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "userName": name,
                "password": password,
                "verifyPassword": verifyPassword
            })
        })
            .then(res => {
                if (res.ok) {
                    toastr.success(`User created!`);
                    registerForm.reset();
                    return;
                }
                return res.json();
            })
            .then(data => {
                if (data.errorMessage) {
                    toastr.error(`${data.errorMessage}`);
                    return;
                }
                if (data.errors.VerifyPassword) {
                    data.errors.VerifyPassword.forEach(element => {
                        toastr.error(`${element}`)
                    });
                }
            })
            .catch(err => {
                console.log(err);
            })
    })
}
const logIn = () => {
    loginForm.addEventListener('submit', (event) => {
        event.preventDefault();
        let username = loginForm.querySelector('#name-inp-log').value;
        let password = loginForm.querySelector('#pass-inp-log').value;

        fetch(`https://localhost:7021/Authorization/Login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "userName": username,
                "password": password,
            })
        })
            .then(res => {
                return res.json();
            })
            .then(data => {
                if (data.errorMessage) {
                    toastr.error(data.errorMessage);
                    return;
                } else if (data.token) {
                    loginForm.reset();
                    user = data;
                    let userString = JSON.stringify(user);
                    sessionStorage.setItem(userKey, userString);
                    window.location = 'person-information.html';
                }
            })
            .catch(err => {
                console.log("Catched: " + err);
            })
    });
}
