<script>
	import './styles.css';
	import Header from '../components/Header.svelte';
	import Footer from "../components/Footer.svelte";
</script>

<div class="app">
	<Header />

	<main>
		<slot />
	</main>

	<Footer />
</div>

<style>
	.app {
		height: 100%;
		display: grid;
		grid-template-rows: auto 1fr auto;
		grid-template-columns: 100%;
		padding-inline: 1rem;
	}

	main {
		flex: 1;
		display: flex;
		flex-direction: column;
		padding: 1rem;
		width: 100%;
		max-width: 64rem;
		margin: 0 auto;
		box-sizing: border-box;
	}

	@media (min-width: 800px) {
		.app {
			width: 800px;
			margin-inline: auto;
		}
	}
</style>
