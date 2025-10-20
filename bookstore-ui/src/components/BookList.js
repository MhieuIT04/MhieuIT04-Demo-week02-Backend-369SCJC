import React from 'react';

function BookList({ books, authors, onEdit, onDelete }) {
    // Tạo một map để dễ dàng tìm tên tác giả từ ID
    const authorMap = authors.reduce((map, author) => {
        map[author.id] = author.name;
        return map;
    }, {});

    return (
        <table className="table mt-4">
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Year</th>
                    <th>Author</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                {books.map(book => (
                    <tr key={book.id}>
                        <td>{book.title}</td>
                        <td>{book.publicationYear}</td>
                        <td>{authorMap[book.authorId] || 'Unknown'}</td>
                        <td>
                            <button className="btn btn-sm btn-warning me-2" onClick={() => onEdit(book)}>Edit</button>
                            <button className="btn btn-sm btn-danger" onClick={() => onDelete(book.id)}>Delete</button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default BookList;