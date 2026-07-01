# 🎮 LupitaCorredora — Jogo de Plataforma 2D

> Projeto final da disciplina de **Game Development** — UniFECAF  
> Desenvolvido na **Unity 6** com **C#**

---

## 📖 Descrição

**LupitaCorredora** é um jogo de plataforma 2D original desenvolvido como peça de portfólio profissional. O jogador controla uma personagem que precisa atravessar três fases com dificuldade crescente, coletando moedas, desviando de inimigos e obstáculos, escalando cordas e chegando até a bandeira no final de cada fase.

A narrativa acompanha uma exploradora que percorre diferentes biomas em busca de fragmentos de cristal para salvar seu vilarejo.

---

## 🖼️ Screenshots

### Tela Menu 
![Menu](screenshots/tela_menu.png)

### Fase 1 — Floresta (Tutorial)
![Fase 1](screenshots/fase1.png)

### Fase 2 — Deserto (Intermediário)
![Fase 1](screenshots/fase2.png)

### Fase 3 — Deserto Avançado (Difícil)
![Fase 1](screenshots/fase3.png)

### Tela Vitória
![Vitória](screenshots/tela_vitoria.png)

### Game Over
![Fim de Jogo](screenshots/tela_game_over.png)

---

## 🕹️ Controles

| Ação | Tecla |
|------|-------|
| Mover | Setas ← → ou A / D |
| Correr | Shift (segurar) |
| Pular | Espaço |
| Escalar | Seta ↑ ↓ (em cordas/escadas) |
| Soltar escalada | Espaço |

---

## ⚙️ Mecânicas Implementadas

- **Movimento completo**: andar, correr, pular e escalar
- **Sistema de vidas**: 3 vidas iniciais com respawn ao perder
- **Pontuação**: acumulada ao coletar moedas
- **HUD**: exibe vidas e pontos em tempo real
- **Coletáveis**: moedas com animação flutuante e efeito sonoro
- **Caixas surpresa**: soltam moedas ao ser atingidas por baixo
- **Inimigos**: patrulha automática entre dois pontos
- **Obstáculos**: espinhos e fogo que tiram vidas
- **Escalada**: cordas e escadas com tag Climbable
- **DeathZone**: zona de morte ao cair do mapa
- **Câmera**: Cinemachine seguindo o personagem
- **Parallax**: efeito de profundidade no background
- **Trilha sonora**: música em loop + efeitos sonoros
- **Transição de fases**: bandeira no final de cada nível

---

## 📋 Fases

### 🌿 Fase 1 — Floresta (Tutorial)
- Terreno plano com grama
- Introdução às mecânicas básicas
- Poucos inimigos e obstáculos
- Ideal para aprender os controles

### 🌵 Fase 2 — Deserto (Intermediário)
- Terreno com buracos e plataformas elevadas
- Mais inimigos e espinhos
- Introdução à mecânica de escalada
- Caixas surpresa com moedas

### 🏜️ Fase 3 — Deserto Avançado (Difícil)
- Nível mais longo e desafiador
- Múltiplos inimigos em posições estratégicas
- Obstáculos combinados (espinhos + buracos)
- Exige domínio completo de todas as mecânicas

---

## 🛠️ Scripts

| Script | Função |
|--------|--------|
| `PlayerController.cs` | Movimento, pulo, corrida e escalada |
| `GameManager.cs` | Singleton — gerencia vidas, pontuação e cenas |
| `HUDManager.cs` | Atualiza a interface do jogador |
| `Collectible.cs` | Itens coletáveis com som e pontuação |
| `HitBox.cs` | Caixas que soltam moedas ao serem atingidas |
| `EnemyPatrol.cs` | Patrulha automática do inimigo |
| `EnemyDamage.cs` | Dano ao player e stomping |
| `Hazard.cs` | Obstáculos com dano e knockback |
| `LevelGoal.cs` | Fim de fase e transição de cena |
| `DeathZone.cs` | Detecta queda do player |
| `ParallaxBackground.cs` | Efeito de parallax no background |

---

## 🎨 Assets Utilizados

- **Sprites**: [Kenney.nl — Pixel Platformer](https://kenney.nl/assets/pixel-platformer) (CC0)
- **Áudio**: [OpenGameArt.org](https://opengameart.org) (CC0)
- **Engine**: Unity 6 (6000.4.7f1)
- **Linguagem**: C#
- **Câmera**: Cinemachine

---

## 🚀 Como Executar

### Jogar o executável:
1. Baixe a pasta compactada `Build.zip`
2. Extraia os arquivos
3. Execute o arquivo `LupitaCorredora.exe`
4. Selecione a resolução desejada e clique em **Play**

### Abrir no Unity:
1. Instale o **Unity Hub** e o **Unity 6**
2. Clone ou baixe este repositório
3. No Unity Hub, clique em **Add** e selecione a pasta do projeto
4. Abra a cena `Assets/Scenes/SampleScene`
5. Clique em **Play** para testar

---

## 📁 Estrutura do Projeto

```
Assets/
├── Animations/          # Animator Controllers
├── Audio/               # Músicas e efeitos sonoros
│   ├── Music/
│   └── SFX/
├── Prefabs/             # Objetos pré-configurados
├── Scenes/              # Cenas do jogo
│   ├── Menu             # Inicial
│   ├── Level_01         # Fase 1
│   ├── Level_02         # Fase 2
│   ├── Level_03         # Fase 3
│   ├── Vitória          # Fase final
│   └── Game Over        # Tente novamente
├── Scripts/             # Código C#
└── Sprites/             # Imagens e spritesheets
```

---

## 👩‍💻 Desenvolvido por

**Jackson Sousa**  
Curso: Analise e Desenvolvimento de Sistemas  
Disciplina: Game Development  
Instituição: UniFECAF  
Ano: 2026

---

## 📝 Licença

Assets visuais e sonoros utilizados sob licença **Creative Commons Zero (CC0)** — uso livre para projetos pessoais e comerciais.
