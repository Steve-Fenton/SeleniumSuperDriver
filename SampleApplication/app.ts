var testForm = document.getElementById('test-form');
var alertLink = document.getElementById('alerter');

testForm.onsubmit = function (ev: Event) {
    ev.preventDefault();

    var name = this.name.value;
    var email = this.email.value;

    this.style.display = 'none';

    document.getElementById('content').innerHTML = 'You entered ' + name + ' and ' + email;

    return false;
};

alertLink.onclick = function (ev: Event) {
    ev.preventDefault();
    alert('Test alert message.');
    return false;
};