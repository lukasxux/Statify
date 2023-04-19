

<template>
    <div>
      <h1>Display your Spotify Profile Data</h1>
  
      <section id="profile">
        <h2>Logged in as <span>{{ displayName }}</span></h2>
        <img id="avatar" width="200" :src="avatarSrc" />
        <ul>
          <li>User ID: <span>{{ id }}</span></li>
          <li>Email: <span>{{ email }}</span></li>
          <li>Spotify URI: <a :href="uri">{{ uri }}</a></li>
          <li>Link: <a :href="url">{{ url }}</a></li>
          <li>Profile Image: <span>{{ imgUrl }}</span></li>
        </ul>
      </section>
    </div>
  </template>



  <script>
  async function redirectToAuthCodeFlow(clientId) {
    const verifier = generateCodeVerifier(128);
    const challenge = await generateCodeChallenge(verifier);

    localStorage.setItem("verifier", verifier);

    const params = new URLSearchParams();
    params.append("client_id", clientId);
    params.append("response_type", "code");
    params.append("redirect_uri", "http://localhost:5173/callback");
    params.append("scope", "user-read-private user-read-email");
    params.append("code_challenge_method", "S256");
    params.append("code_challenge", challenge);

    document.location = `https://accounts.spotify.com/authorize?${params.toString()}`;
  }

  async function getAccessToken(clientId, code) {
    const verifier = localStorage.getItem("verifier");

    const params = new URLSearchParams();
    params.append("client_id", clientId);
    params.append("grant_type", "authorization_code");
    params.append("code", code);
    params.append("redirect_uri", "http://localhost:5173/callback");
    params.append("code_verifier", verifier);

    const result = await fetch("https://accounts.spotify.com/api/token", {
      method: "POST",
      headers: { "Content-Type": "application/x-www-form-urlencoded" },
      body: params,
    });

    const { access_token } = await result.json();
    return access_token;
  }

  function generateCodeVerifier(length) {
    let text = "";
    let possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    for (let i = 0; i < length; i++) {
      text += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return text;
  }

  async function generateCodeChallenge(codeVerifier) {
    const data = new TextEncoder().encode(codeVerifier);
    const digest = await window.crypto.subtle.digest("SHA-256", data);
    return btoa(String.fromCharCode.apply(null, [...new Uint8Array(digest)]))
      .replace(/\+/g, "-")
      .replace(/\//g, "_")
      .replace(/=+$/, "");
  }

  const clientId = "afc8cff8760e496a82a85b2cf42ff99b";
  const params = new URLSearchParams(window.location.search);
  const code = params.get("code");

  if (!code) {
    redirectToAuthCodeFlow(clientId);
  } else {
    getAccessToken(clientId, code)
      .then((accessToken) => fetchProfile(accessToken))
      .then((profile) => populateUI(profile));
  }

  async function fetchProfile(code) {
    const result = await fetch("https://api.spotify.com/v1/me", {
      method: "GET",
      headers: { Authorization: `Bearer ${code}` },
    });

    return await result.json();
  }

  function populateUI(profile) {
    // Populate UI with profile data
    const displayName = profile.display_name;
    const avatarSrc = profile.images[0].url;
    const id = profile.id;
    const email = profile.email;
    const uri = profile.uri;
    const url = profile.external_urls.spotify;
    const imgUrl = profile.images[0].url;

    console.log(displayName);
    console.log(avatarSrc);
    console.log(id);
    console.log(email);
    console.log(uri);
    console.log(url);
    console.log(imgUrl);
  }
</script>