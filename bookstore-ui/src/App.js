import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import AuthorList from './components/AuthorList';
import AuthorForm from './components/AuthorForm';
import BookList from './components/BookList';
import BookForm from './components/BookForm';

function App() {
    const [authors, setAuthors] = useState([]);
    const [books, setBooks] = useState([]);
    const [currentAuthor, setCurrentAuthor] = useState(null); // Để sửa tác giả
    const [currentBook, setCurrentBook] = useState(null); // Để sửa sách

    const API_BASE_URL = 'https://localhost:7091/api'; // << NHỚ THAY ĐỔI CỔNG NÀY

    // Hàm gọi lại để làm mới dữ liệu
    const fetchData = async () => {
        try {
            const authorsRes = await fetch(`${API_BASE_URL}/authors`);
            const authorsData = await authorsRes.json();
            setAuthors(authorsData);

            const booksRes = await fetch(`${API_BASE_URL}/books`);
            const booksData = await booksRes.json();
            setBooks(booksData);
        } catch (error) {
            console.error("Error fetching data:", error);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    // --- Xử lý cho Author ---
    const handleSaveAuthor = async (author) => {
        const method = author.id ? 'PUT' : 'POST';
        const url = author.id ? `${API_BASE_URL}/authors/${author.id}` : `${API_BASE_URL}/authors`;

        await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(author),
        });
        setCurrentAuthor(null);
        fetchData(); // Tải lại dữ liệu
    };

    const handleEditAuthor = (author) => {
        setCurrentAuthor(author);
    };

    const handleDeleteAuthor = async (authorId) => {
        if (window.confirm('Are you sure you want to delete this author?')) {
            await fetch(`${API_BASE_URL}/authors/${authorId}`, { method: 'DELETE' });
            fetchData(); // Tải lại dữ liệu
        }
    };

    // --- Xử lý cho Book ---
    const handleSaveBook = async (book) => {
        const method = book.id ? 'PUT' : 'POST';
        const url = book.id ? `${API_BASE_URL}/books/${book.id}` : `${API_BASE_URL}/books`;

        // Chuyển đổi authorId thành số nguyên
        const bookPayload = { ...book, authorId: parseInt(book.authorId, 10) };

        await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(bookPayload),
        });
        setCurrentBook(null);
        fetchData();
    };

    const handleEditBook = (book) => {
        setCurrentBook(book);
    };

    const handleDeleteBook = async (bookId) => {
        if (window.confirm('Are you sure you want to delete this book?')) {
            await fetch(`${API_BASE_URL}/books/${bookId}`, { method: 'DELETE' });
            fetchData();
        }
    };


    return (
        <div className="container mt-4">
            <div className="row">
                <div className="col-md-6">
                    <h2>Authors</h2>
                    <AuthorForm currentAuthor={currentAuthor} onSave={handleSaveAuthor} onCancel={() => setCurrentAuthor(null)} />
                    <AuthorList authors={authors} onEdit={handleEditAuthor} onDelete={handleDeleteAuthor} />
                </div>
                <div className="col-md-6">
                    <h2>Books</h2>
                    <BookForm currentBook={currentBook} authors={authors} onSave={handleSaveBook} onCancel={() => setCurrentBook(null)} />
                    <BookList books={books} authors={authors} onEdit={handleEditBook} onDelete={handleDeleteBook} />
                </div>
            </div>
        </div>
    );
}

export default App;