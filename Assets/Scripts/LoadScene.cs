using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System; // Adicionado para usar Action

public class LoadScene : MonoBehaviour
{
    // O padrão Singleton para acesso global fácil
    public static LoadScene Instance { get; private set; }

    // Opcional: Evento que a UI pode assinar para atualizar a barra de progresso
    public Action<float> OnProgressUpdate; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // É essencial manter este objeto vivo durante a transição
            // para que a Coroutine não pare se você carregar uma cena de loading antes.
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método público que outros scripts (como MainMenuUI) chamarão
    public void StartLoad(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncAndHandle(sceneName));
    }

    public IEnumerator LoadSceneAsyncAndHandle(string sceneName)
    {
        // 1. Inicia o carregamento assíncrono
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        // 2. Impede que a cena carregada seja ativada automaticamente
        operation.allowSceneActivation = false; 

        // 3. Loop de espera e monitoramento de progresso
        while (!operation.isDone)
        {
            // progress: 0.0 -> 0.9 (o clamp garante que não passe de 1.0)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            // Notifica qualquer script inscrito (UI) sobre o progresso
            OnProgressUpdate?.Invoke(progress); 

            // Quando o carregamento estiver em 90% (o máximo antes da ativação)
            if (operation.progress >= 0.9f)
            {
                // Se o carregamento estiver pronto e você não tiver uma tela de loading para gerenciar...
                
                // Exemplo: Esperar 1 segundo antes de liberar, para evitar transição muito rápida
                // yield return new WaitForSeconds(1f); 
                
                // Ativa a cena. O Unity fará a transição e a cena começará a rodar.
                operation.allowSceneActivation = true;
            }
            
            yield return null; 
        }
    }
}