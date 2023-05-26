<template>
    <div class="container">
        <div class="row ">
            <div class="col">
                <img class="img" src="../assets/img/StatifyLogo.png" alt="" />
            </div>
            <div id="welcome" class="col d-flex align-items-center">
                <div>
                    <h1>Welcome to <span style="color: #1db954;">S</span><span style="color: #1db954;">t</span><span style="color: #1db954;">a</span><span style="color: #1db954;">t</span>ify</h1>
                    <p>Statify is a web application that allows you to view your Spotify listening history and statistics.</p>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
.img{
    width: 600px;
}
button{
    width: 150px;
    height: 50px;
    border-radius: 10px;
    background-color: #1db954;
    font-size: 15px;
    font-weight: bold
}
.container {
    position: relative;
    text-align: center;
    color: white;
}
#welcome{
    text-align: left;
}
</style>