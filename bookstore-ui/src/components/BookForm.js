import React, { useState, useEffect } from 'react';

function BookForm({ currentBook, authors, onSave, onCancel }) {
    const [title, setTitle] = useState('');
    const [publicationYear, setPublicationYear] = useState('');
    const [authorId, setAuthorId] = useState('');

    useEffect(() => {
        if (currentBook) {
            setTitle(currentBook.title);
            setPublicationYear(currentBook.publicationYear);
            setAuthorId(currentBook.authorId);
        } else {
            setTitle('');
            setPublicationYear('');
            setAuthorId('');
        }
    }, [currentBook]);

    const handleSubmit = (e) => {
        e.preventDefault();
        onSave({ ...currentBook, title, publicationYear, authorId });
    };

    return (
        <form onSubmit={handleSubmit} className="mb-4">
            <h4>{currentBook ? 'Edit Book' : 'Add New Book'}</h4>
            <div className="mb-2">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Title"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    required
                />
            </div>
            <div className="mb-2">
                <input
                    type="number"
                    className="form-control"
                    placeholder="Publication Year"
                    value={publicationYear}
                    onChange={(e) => setPublicationYear(e.target.value)}
                    required
                />
            </div>
            <div className="mb-2">
                <select
                    className="form-select"
                    value={authorId}
                    onChange={(e) => setAuthorId(e.target.value)}
                    required
                >
                    <option value="" disabled>Select an author</option>
                    {authors.map(author => (
                        <option key={author.id} value={author.id}>{author.name}</option>
                    ))}
                </select>
            </div>
            <button type="submit" className="btn btn-primary me-2">{currentBook ? 'Update' : 'Save'}</button>
            {currentBook && <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancel</button>}
        </form>
    );
}

export default BookForm;