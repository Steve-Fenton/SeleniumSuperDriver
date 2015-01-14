var testForm = document.getElementById('test-form');

testForm.onsubmit = function (ev: Event) {
    ev.preventDefault();

    var name = this.name.value;
    var email = this.email.value;

    this.style.display = 'none';

    document.getElementById('content').innerHTML = 'You entered ' + name + ' and ' + email;

    return false;
};