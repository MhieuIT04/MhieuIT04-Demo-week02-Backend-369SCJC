import React, { useState, useEffect } from 'react';

function AuthorForm({ currentAuthor, onSave, onCancel }) {
    const [name, setName] = useState('');
    const [birthDate, setBirthDate] = useState('');

    useEffect(() => {
        if (currentAuthor) {
            setName(currentAuthor.name);
            // Định dạng lại ngày để input type="date" có thể hiển thị
            setBirthDate(new Date(currentAuthor.birthDate).toISOString().split('T')[0]);
        } else {
            setName('');
            setBirthDate('');
        }
    }, [currentAuthor]);

    const handleSubmit = (e) => {
        e.preventDefault();
        onSave({ ...currentAuthor, name, birthDate });
    };

    return (
        <form onSubmit={handleSubmit} className="mb-4">
            <h4>{currentAuthor ? 'Edit Author' : 'Add New Author'}</h4>
            <div className="mb-2">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    required
                />
            </div>
            <div className="mb-2">
                <input
                    type="date"
                    className="form-control"
                    value={birthDate}
                    onChange={(e) => setBirthDate(e.target.value)}
                    required
                />
            </div>
            <button type="submit" className="btn btn-primary me-2">{currentAuthor ? 'Update' : 'Save'}</button>
            {currentAuthor && <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancel</button>}
        </form>
    );
}

export default AuthorForm;