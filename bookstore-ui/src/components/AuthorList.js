import React from 'react';

function AuthorList({ authors, onEdit, onDelete }) {
    return (
        <table className="table mt-4">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Birth Date</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {authors.map(author => (
                    <tr key={author.id}>
                        <td>{author.id}</td>
                        <td>{author.name}</td>
                        <td>{new Date(author.birthDate).toLocaleDateString()}</td>
                        <td>
                            <button className="btn btn-sm btn-warning me-2" onClick={() => onEdit(author)}>Edit</button>
                            <button className="btn btn-sm btn-danger" onClick={() => onDelete(author.id)}>Delete</button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default AuthorList;