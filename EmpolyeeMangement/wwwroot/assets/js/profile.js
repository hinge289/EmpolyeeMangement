document.addEventListener('DOMContentLoaded', () => {
    const profileForm = document.getElementById('profileForm');
    const editBtn = document.getElementById('editBtn');
    const updateBtn = document.getElementById('updateBtn');
    const cancelBtn = document.getElementById('cancelBtn');
    const formInputs = profileForm.querySelectorAll('input, select');

    const originalValues = new Map([...formInputs].map(input => [input.id, input.value]));

    const toggleEdit = (enable) => {
        formInputs.forEach(input => {
            input.readOnly = !enable;
            input.disabled = !enable;
            if (!enable) input.value = originalValues.get(input.id);
        });
        editBtn.classList.toggle('d-none', enable);
        updateBtn.classList.toggle('d-none', !enable);
        cancelBtn.classList.toggle('d-none', !enable);
    };

    editBtn.addEventListener('click', () => toggleEdit(true));
    cancelBtn.addEventListener('click', () => toggleEdit(false));
});