//Используем состояние React (use React)
import React, { useState } from 'react'
import Constants from '../utilities/Constants'

export default function PostUpdateForm(props) {

    const initialFormData = Object.freeze({
        text: props.post.text,
        from: props.post.from,
        date: props.post.date,
    });


    //Передаем некоторые начальные данные формы
    const [formData, setFormData] = useState(initialFormData);
    const [posts, setPosts] = useState([]);

    const handleChange = (e) => {
        //Установка данных формы
        setFormData({
            ...formData,
            [e.target.name]: e.target.value,
        });
    };



    const handleSubmit = (e) => {
        //Предотвратит выполнение действия по умолчанию при отправки формы
        e.preventDefault();

        const postToUpdate = {
            date: formData.date,
        };
        //URL
        const url = Constants.API_URL_UPDATE_POST;

        fetch(url, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postToUpdate)
        })
            .then(Response => Response.json())
            .then(responseFromServer => {
                console.log(responseFromServer);
            })
            .catch((error) => {
                console.log(error);
                alert(error);
            });

        props.onPostUpdated(postToUpdate);
    };

    function deletePost(postId) {
        const url = `${Constants.API_URL_DELETE_POST_BY_ID}/${postId}`;

        fetch(url, {
            method: 'DELETE'
        })
            .then(Response => Response.json())
            .then(responseFromServer => {
                console.log(responseFromServer);
                onPostDeleted(postId);
            })
            .catch((error) => {
                console.log(error);
                alert(error);
            });
    }

    return (
        <form className='w-100 px-5'>
            <center><h1 className='mt-5'>Работа с задачей</h1></center>
            <div className='table-responsive mt-5'>
                <table className='table table-bordered border-dark'>
                    <thead>
                        <tr>
                            <th scope="col">Дата</th>
                            <th scope="col">Статус</th>
                            <th scope="col">Кому</th>
                            <th scope="col">Описание</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{formData.date}</td>
                            <td>{formData.date}</td>
                            <td>{formData.from}</td>
                            <td>{formData.text}</td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <button onClick={handleSubmit} className="btn btn-dark btn-lg w-200 mt-5">Открепить от исполнителя</button>
            <br />
            <button onClick={() => props.onPostUpdated(null)} className="btn btn-dark btn-lg w-200 mt-3" >Приостановить</button>
            <br />
            <button onClick={() => { if (window.confirm(`Are you sure ${formData.text}`)) deletePost(props.post.postId) }} className="btn btn-dark btn-lg w-200 mt-3">Завершить</button>
        </form>
    )

    function onPostDeleted(deletePostPostId) {
        let postsCopy = [...posts];

        const index = postsCopy.findIndex((postsCopyPost, currentIndex) => {
            if (postsCopyPost.postId === deletePostPostId) {
                return true;
            }
        });

        if (index !== -1) {
           postsCopy.splice(index, 1);
        }

        setPosts(postsCopy);

        alert(`Post successfully`);
    }
}
