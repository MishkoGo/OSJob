import React, { useState } from "react";
import Constants from "./utilities/Constants";
import PostCreateForm from "./components/PostCreateForm";
import PostUpdateForm from "./components/PostUpdateForm";

export default function App() {
  //Состояние публикации
  const [posts, setPosts] = useState([]);
  const [showingCreateNewPostForm, setShowingCreateNewPostForm] = useState(false); //Отображение формы
  const [postCurrentlyBeingUpdated, setPostCurrentlyBeingUpdated] = useState(null);//Используем обьект, который содержит пост, который в данный момент обновляется

  function getPosts() {
    const url = Constants.API_URL_GET_ALL_POSTS;

    //Делаем запрос на выборку
    fetch(url, {
      method: 'GET'
    })
      .then(Response => Response.json())
      .then(postsFromServer => {
        setPosts(postsFromServer);
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  return (
    <div className="container">
      <div className="row min-vh-100">
        <div className="col d-flex flex-column justify-content-center align-items-center">
          {(showingCreateNewPostForm === false && postCurrentlyBeingUpdated === null) && (
            <div>
              <center> <h1>Задачи</h1></center>

              <div className="mt-5">
                <button onClick={getPosts} className="btn btn-dark btn-lg w-100">Получить все задачи</button>
                <button onClick={() => {setShowingCreateNewPostForm(true)}} className="btn btn-secondary btn-lg w-100 mt-4">Создать новую задачу</button>
              </div>
            </div>
          )}

          {(posts.length > 0 && showingCreateNewPostForm === false && postCurrentlyBeingUpdated === null) && renderPostsTable()}

          {showingCreateNewPostForm && <PostCreateForm onPostCreated={onPostCreated} />}
        
          {postCurrentlyBeingUpdated !== null && <PostUpdateForm post = {postCurrentlyBeingUpdated} onPostUpdated = {onPostUpdated} />}
        </div>
      </div>
    </div>
  );

  //функция которая возвращает таблицу JSX
  function renderPostsTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">PostId (PK)</th>
              <th scope="col">Дата</th>
              <th scope="col">От кого</th>
              <th scope="col">Кому</th>
              <th scope="col">Описание</th>
              <th scope="col">PostId </th>
            </tr>
          </thead>
          <tbody>
            {posts.map((post) => (
              <tr key={post.postId}>
                <th scope="row">{post.postId}</th>
                <td>{post.date}</td>
                <td>{post.from}</td>
                <td>{post.to}</td>
                <td>{post.text}</td>
                
                <td>
                  <button onClick={() => setPostCurrentlyBeingUpdated(post)} className="btn btn-dark btn-lg mx-3 my-3">Изменить</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        <button onClick={() => setPosts([])} className="btn btn-dark btn-lg w-100">Empty</button>
      </div>
    )
  }

  function onPostCreated(createdPost) {
    setShowingCreateNewPostForm(false);

    if (createdPost === null) {
      return;
    }

    alert(`Post successfully created. After clicking OK, your new post tilted "${createdPost.title}" will show up in the table below.`);

    getPosts();
  }

   //Функция, которая вызывается из компонента формы после обновления
  function onPostUpdated(updatedPost){
    setPostCurrentlyBeingUpdated(null);

    if(updatedPost === null){
      return;
    }
    //делаем копию массива сообщений,сохраняем обновление поста в массиве прямо в памяти
    let postsCopy = [...posts];

    const index = postsCopy.findIndex((postsCopyPost, currentIndex) => {
      if(postsCopyPost.postId === updatedPost.postId){
        return true;
      }
    });

    if(index !== -1){
      //Устанавливаем postsCopy по индексу нашего индекса в обновленный пост
      postsCopy[index] = updatedPost;
    }

    setPosts(postsCopy);

    alert(`Post successfully updated. After clicking OK, look for the post with the title "${updatedPost.text}" in the table below to see the updates.`);
  }
}


