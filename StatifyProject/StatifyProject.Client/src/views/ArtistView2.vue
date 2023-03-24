<script setup>
import axios from 'axios';
</script>

<template>
    <div class="artistView">
        <h1>Known Artists</h1>
        <p v-if="authenticated">Click on the artist to make this as your favorite!</p>
        <p v-else>Please log in to set your favourite artist.</p>
        <div v-bind:class="{ artistbox: true, favorite: a.isFavorite }" v-for="a in artists" v-bind:key="a.guid" v-on:click="makeFavorite(a)">
            <h4>{{ a.name }}</h4>
            <div class="songbox" v-for="s in a.songs" v-bind:key="s.guid">
                <div class="songtitle">{{ s.title }}</div>
                <div class="songduration">{{ s.length }}</div>
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

.artistbox {
    padding: 1em;
    text-align: left;
    margin-bottom: 1em;
}

.artistbox:hover {
    background-color: hsl(0, 0%, 21%);
}

.songbox {
    display: flex;
    margin-left: 1em;
}

.songtitle {
    flex-grow: 1;
}

.favorite {
    border: 1px solid hsl(128, 100%, 20%);
}
</style>

<script>
export default {
    data() {
        return {
            artists: [],
        };
    },
    async mounted() {
        var response = await axios.get('artist');
        this.artists = response.data;
    },
    methods: {
        async makeFavorite(artist) {
            if (!this.authenticated) {
                return;
            }
            try {
                var response = await axios.put('user/setArtist/' + artist.guid);
                var response = await axios.get('artist');
                this.artists = response.data;
            } catch {
                alert('Fehler beim Setzen des favorite Artists.');
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