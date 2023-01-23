<script setup>
import axios from 'axios';
</script>

<template>
    <div class="songView">
        <h1>Known Songs</h1>
        <p v-if="authenticated">Click on the song to make this as your favorite!</p>
        <p v-else>Please log in to set your favourite song.</p>
        <div v-bind:class="{ songbox: true, favorite: s.isFavorite }" v-for="s in songs" v-bind:key="s.guid" v-on:click="makeFavorite(s)">
            <div class="songimage"><img v-bind:src="s.imageUrl" /></div>
            <div class="songinfo">
                <div class="row1">
                    <div class="songtitle"> <span class="me-3">{{ s.length }}</span><span class="title">{{ s.title }}</span></div>
                    <div class="songlink">
                        <a target="_blank" v-bind:href="s.link"> Watch on youtube </a>
                    </div>
                </div>
                <div class="row2">Liked by {{ s.likedByCount }} Users</div>
            </div>
        </div>
    </div>
</template>

<style scoped>
table {
    color: white;
}

.textlink {
    cursor: pointer;
}

.songbox {
    padding: 1em;
    text-align: left;
    margin-bottom: 1em;
    display: flex;
    gap: 1em;
}

.songimage img {
    width: 100px;
}

.songbox:hover {
    background-color: hsl(0, 0%, 21%);
}

.favorite {
    border: 1px solid hsl(128, 100%, 20%);
}

.songinfo {
    flex-grow: 1;
}

.songtitle {
    flex-grow: 1;
}

.row1 {
    display: flex;
}

.title {
    font-size: 150%;
}
a {
    text-decoration: none;
    color: hsl(96, 100%, 50%);
}
</style>

<script>
export default {
    data() {
        return {
            songs: [],
        };
    },
    async mounted() {
        var response = await axios.get('song');
        this.songs = response.data;
    },
    methods: {
        async makeFavorite(song) {
            if (!this.authenticated) {
                return;
            }
            try {
                var response = await axios.put('user/setSong/' + song.guid);
                var response = await axios.get('song');
                this.songs = response.data;
            } catch {
                alert('Fehler beim Setzen des favorite songs.');
            }
        },
    },
    computed: {
        authenticated() {
            return this.$store.state.user != null;
        },
        userinfo() {
            return this.$store.state.user;
        },
    },
};
</script>