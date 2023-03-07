//Используем состояние React (use React)
import React, { useState } from 'react'
import Constants from '../utilities/Constants'

export default function PostCreateForm(props) {
    const initialFormData = Object.freeze({
        text: "12",
        from: "Zavod",
        whom: "Ai"
    });

    //Передаем некоторые начальные данные формы
    const [formData, setFormData] = useState(initialFormData);

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

        const postToCreate = {
            postId: 0,
            text: formData.text,
            date: formData.date,
            whom: formData.whom,
            from: formData.from
        };
        //URL
        const url = Constants.API_URL_CREATE_POST;

        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(postToCreate)
        })
            .then(Response => Response.json())
            .then(responseFromServer => {
                console.log(responseFromServer);
            })
            .catch((error) => {
                console.log(error);

                alert(error);
            });

        props.onPostCreated(postToCreate);
    };

    return (
        <form className='w-100 px-5'>
            <center><h1 className='mt-5'>Создать задачу</h1></center>
            <div className='mt-5'>
                <label className='h3 form-label'> Дата </label>
                <input value={FormData.date} name="date" type="text" className='form-control' onChange={handleChange} />
            </div>
            <div className='mt-4'>
                <label className='h3 form-label'> От кого </label>
                <input value={FormData.from} name="from" type="text" className='form-control' onChange={handleChange} />
            </div>
            <div className='mt-3'>
                <label className='h3 form-label'> Кому </label>
                <input value={FormData.whom} name="whom" type="text" className='form-control' onChange={handleChange} />
            </div>
            <div className='mt-2'>
                <label className='h3 form-label'>Описание</label>
                <input value={FormData.text} name="text" type="text" className='form-control' onChange={handleChange} />
            </div>

            <button onClick={handleSubmit} className="btn btn-dark btn-lg w-100 mt-5">Создать</button>
            <button onClick={() => props.onPostCreated(null)} className="btn btn-secondary btn-lg w-100 mt-3" >Отмена</button>
        </form>
    )
}
